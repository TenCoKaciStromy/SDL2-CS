using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObjectiveSdl2.Core {
	public sealed class SdlEventLoop {
		private readonly object syncRoot = new object();
		public object SyncRoot => this.syncRoot;

		private bool isRunning;
		public bool IsRunning => this.isRunning;

		public bool Run() {
			lock (this.syncRoot) {
				if (this.isRunning) { return false; }

				try {
					this.isRunning = true;
					Monitor.Exit(this.syncRoot);
					try {
						this.RunInternal();
						return true;
					}
					finally {
						Monitor.Enter(this.syncRoot);
					}
				}
				finally {
					this.isRunning = false;
				}
			}
		}

		private void RunInternal() {
			bool quit = false;
			while (!quit) {
				while (0 < SDL2.SDL.SDL_PollEvent(out var _event)) {
					bool stopHandling = false;
					foreach (var handler in this.eventHandlers) {
						if (handler(_event)) {
							stopHandling = true;
							break;
						}
					}
					if (stopHandling) {
						continue;
					}

					if (_event.type == SDL2.SDL.SDL_EventType.SDL_QUIT) {
						quit = true;
						break;
					}
				}

				this.Idle?.Invoke(this, EventArgs.Empty);
			}
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
