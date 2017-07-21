using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ObjectiveSdl2.Core.Registers;

namespace ObjectiveSdl2.Core {
	public sealed class SdlEventLoop {
		private readonly object syncRoot = new object();
		public object SyncRoot => this.syncRoot;

		private bool isRunning;
		public bool IsRunning => this.isRunning;

		private SdlContext context;
		public SdlContext Context => this.context;

		public bool Run(SdlContext context) {
			if (context is null) { throw new ArgumentNullException(nameof(context)); }

			lock (this.syncRoot) {
				if (this.isRunning) { return false; }

				try {
					this.isRunning = true;
					Monitor.Exit(this.syncRoot);
					try {
						this.context = context;
						this.PrepareRegisters();

						this.RunInternal();
						return true;
					}
					finally {
						Monitor.Enter(this.syncRoot);
					}
				}
				finally {
					this.isRunning = false;
					this.context = null;
					this.ClearRegisters();
				}
			}
		}

		private WindowsRegister regWindows;
		private void PrepareRegisters() {
			this.regWindows = context.Registers.Windows;
		}
		private void ClearRegisters() {
			this.regWindows = null;
		}

		private void RunInternal() {
			bool quit = false;
			while (!quit) {
				while (0 < SDL2.SDL.SDL_PollEvent(out var _event)) {
					bool skipHandling = false;
					foreach (var handler in this.eventHandlers) {
						var rslt = handler(_event);
						if (SdlEventHandlerResult.Skip == rslt) {
							skipHandling = true;
						}
						else if (SdlEventHandlerResult.SkipAndStop == rslt) {
							skipHandling = true;
							break;
						}
					}
					if (skipHandling) {
						continue;
					}

					var eType = _event.type;
					if (SDL2.SDL.SDL_EventType.SDL_QUIT == eType) {
						quit = true;
						break;
					}
					else if (SDL2.SDL.SDL_EventType.SDL_WINDOWEVENT == eType) {
						this.HandleWindowEvent(_event.window);
					}
				}

				if (!quit) {
					this.Idle?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void HandleWindowEvent(SDL2.SDL.SDL_WindowEvent winEvent) {
			var window = this.regWindows.Find(winEvent.windowID);
			if (window is null) { return; }

			window.HandleLoopEvent(winEvent);
		}

		public event EventHandler Idle;

		private readonly LinkedList<SdlEventHandler> eventHandlers = new LinkedList<SdlEventHandler>();
		public event SdlEventHandler EventHandlers {
			add {
				Monitor.Enter(this.eventHandlers);
				var lastNode = this.eventHandlers.Last;
				if (lastNode is null) {
					this.eventHandlers.AddLast(value);
					Monitor.Exit(this.eventHandlers);
					return;
				}

				Monitor.Enter(lastNode);
				Monitor.Exit(this.eventHandlers);
				this.eventHandlers.AddLast(value);
				Monitor.Exit(lastNode);
			}
			remove {
				Monitor.Enter(this.eventHandlers);
				var node = this.eventHandlers.First;
				if (node is null) {
					Monitor.Exit(this.eventHandlers);
					return;
				}

				Monitor.Enter(node);
				Monitor.Exit(this.eventHandlers);
				while (true) {
					if (value == node.Value) {
						this.eventHandlers.Remove(node);
						Monitor.Exit(node);
						break;
					}
					else {
						var nextNode = node.Next;
						if (nextNode is null) {
							Monitor.Exit(node);
							return;
						}
						Monitor.Enter(nextNode);
						Monitor.Exit(node);
						node = nextNode;
					}
				}
			}
		}
	}
}
