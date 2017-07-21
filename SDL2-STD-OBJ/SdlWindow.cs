using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Core.SafeHandles;

namespace ObjectiveSdl2 {
	public sealed partial class SdlWindow : SdlObject {
		public SdlWindow(SafeHandle validHandle) : base(validHandle) { }
		public SdlWindow(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }
		public SdlWindow(IntPtr validHandle, bool ownsHandle, string title, SdlVector position, SdlVector size) : base(validHandle, ownsHandle) {
			this.title = title;
			this.position = position;
			this.size = size;
		}

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => new SdlWindowSafeHandle(validHandle, ownsHandle);
	}

	partial class SdlWindow {
		public static SdlWindow Create() {
			return Create(string.Empty);
		}
		public static SdlWindow Create(string title) {
			return Create(title, new SdlVector(320, 200));
		}

		public static SdlWindow Create(string title, SdlVector size) {
			return Create(title, new SdlVector(0, 0), size);
		}

		public static SdlWindow Create(string title, SdlVector position, SdlVector size) {
			return Create(title, position, size, SdlWindowCreationFlags.Resizable);
		}

		public static SdlWindow Create(string title, SdlVector position, SdlVector size, SdlWindowCreationFlags creationFlags) {
			var ptr = SDL2.SDL.SDL_CreateWindow(title, position.X, position.Y, size.X, size.Y, (SDL2.SDL.SDL_WindowFlags)(uint)creationFlags);
			var result = new SdlWindow(ptr, true);
			SdlContext.Default.Registers.Windows.Register(result);

			return result;
		}
	}

	partial class SdlWindow : SdlWindow.IInternals {
		#region Internals
		public interface IInternals {
			uint WindowID { get; }
		}
		public IInternals Internals => this;
		#endregion Internals

		#region WindowID
		private uint windowID;
		internal uint WindowID {
			get {
				var result = windowID;
				if (0 == result) {
					this.windowID = result = this.GetWindowID();
				}

				return result;
			}
		}
		uint IInternals.WindowID => this.WindowID;

		private static uint? TryGetWindowID(IntPtr ptrWindow) {
			return SDL2.SDL.SDL_GetWindowID(ptrWindow);
		}
		private uint GetWindowID() {
			return SdlCallUtil.ThrowIfSdlFuncFails(TryGetWindowID, this.GetPointer()).Value;
		}
		#endregion WindowID
	}

	partial class SdlWindow {
		#region Title
		private string title;
		public string Title {
			get => this.title;
			set {
				var okValue = value ?? string.Empty;
				this.title = okValue;
				this.SetWindowTitle(okValue);
			}
		}
		public string TitleRefresh() => this.title = this.GetWindowTitle();

		private static string TryGetWindowTitle(IntPtr ptrWindow) => SDL2.SDL.SDL_GetWindowTitle(ptrWindow);
		private string GetWindowTitle() => SdlCallUtil.ThrowIfSdlFuncFails(TryGetWindowTitle, this.GetPointer());

		private static void TrySetWindowTitle(IntPtr ptrWindow, string value) => SDL2.SDL.SDL_SetWindowTitle(ptrWindow, value);
		private void SetWindowTitle(string value) {
			SdlCallUtil.ThrowIfSdlActionFails(TrySetWindowTitle, this.GetPointer(), value);
		}
		#endregion Title

		#region Position
		private SdlVector position;
		public SdlVector Position {
			get => this.position;
			set {
				this.RaiseBeforeMove();

				this.position = value;
				this.SetWindowPosition(value.X, value.Y);
			}
		}
		public SdlVector PositionRefresh() => this.position = this.GetWindowPosition();

		private static SdlVector TryGetWindowPosition(IntPtr ptrWindow) {
			SDL2.SDL.SDL_GetWindowPosition(ptrWindow, out var x, out var y);
			return new SdlVector(x, y);
		}
		private SdlVector GetWindowPosition() {
			return SdlCallUtil.ThrowIfSdlFuncFails(TryGetWindowPosition, this.GetPointer());
		}

		private static void TrySetWindowPosition(IntPtr ptrWindow, int x, int y) {
			SDL2.SDL.SDL_SetWindowPosition(ptrWindow, x, y);
		}
		private void SetWindowPosition(int x, int y) {
			SdlCallUtil.ThrowIfSdlActionFails(TrySetWindowPosition, this.GetPointer(), x, y);
		}

		private void HandleMoved(SDL2.SDL.SDL_WindowEvent @event) {
			this.PositionRefresh();
			this.RaiseAfterMove();
		}

		public event EventHandler BeforeMove;
		private void RaiseBeforeMove() => this.BeforeMove?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterMove;
		private void RaiseAfterMove() => this.AfterMove?.Invoke(this, EventArgs.Empty);
		#endregion Position

		#region Size
		private SdlVector size;
		public SdlVector Size {
			get => this.size;
			set {
				this.size = value;
				this.SetWindowSize(value.X, value.Y);
			}
		}
		public SdlVector SizeRefresh() => this.size = this.GetWindowSize();

		private static SdlVector TryGetWindowSize(IntPtr ptrWindow) {
			SDL2.SDL.SDL_GetWindowSize(ptrWindow, out var w, out var h);
			return new SdlVector(w, h);
		}
		private SdlVector GetWindowSize() {
			return SdlCallUtil.ThrowIfSdlFuncFails(TryGetWindowSize, this.GetPointer());
		}

		private static void TrySetWindowSize(IntPtr ptrWindow, int widht, int height) {
			SDL2.SDL.SDL_SetWindowSize(ptrWindow, widht, height);
		}
		private void SetWindowSize(int widht, int height) {
			SdlCallUtil.ThrowIfSdlActionFails(TrySetWindowSize, this.GetPointer(), widht, height);
		}

		private void HandleSizeChanged(SDL2.SDL.SDL_WindowEvent @event) {
			this.SizeRefresh();
			this.RaiseAfterSizeChange();
		}

		public event EventHandler BeforeSizeChange;
		private void RaiseBeforeSizeChange() => this.BeforeSizeChange?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterSizeChange;
		private void RaiseAfterSizeChange() => this.AfterSizeChange?.Invoke(this, EventArgs.Empty);

		private void HandleResized(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterResize();
		}

		public event EventHandler BeforeResize;
		private void RaiseBeforeResize() => this.BeforeResize?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterResize;
		private void RaiseAfterResize() => this.AfterResize?.Invoke(this, EventArgs.Empty);
		#endregion Size
	}

	partial class SdlWindow {
		#region Renderer
		private SdlRenderer renderer;
		public SdlRenderer Renderer {
			get {
				var result = this.renderer;
				if (result?.IsValid() ?? false) {
					return result;
				}

				var ptrSelf = this.GetPointer();
				result = SdlRenderer.Get(ptrSelf);
				if (result is null) {
					return this.ResetRenderer();
				}
				this.renderer = result;
				return result;
			}
		}

		public SdlRenderer ResetRenderer() {
			return this.ResetRenderer(SdlRenderingFlags.Software);
		}
		public SdlRenderer ResetRenderer(SdlRenderingFlags renderingFlags) {
			var prevRenderer = this.renderer;
			if (!(prevRenderer is null)) {
				prevRenderer.Dispose();
				this.renderer = null;
			}

			var result = SdlRenderer.Create(this.GetPointer(), renderingFlags);
			this.renderer = result;
			return result;
		}
		#endregion Renderer
	}

	partial class SdlWindow {
		#region Show
		private static void TryShowWindow(IntPtr ptrWindow) {
			SDL2.SDL.SDL_ShowWindow(ptrWindow);
		}
		public void Show() {
			this.RaiseBeforeShow();
			SdlCallUtil.ThrowIfSdlActionFails(TryShowWindow, this.GetPointer());
		}

		private void HandleShown(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterShow();
		}

		public event EventHandler BeforeShow;
		private void RaiseBeforeShow() => this.BeforeShow?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterShow;
		private void RaiseAfterShow() => this.AfterShow?.Invoke(this, EventArgs.Empty);
		#endregion Show

		#region Raise
		private static void TryRaiseWindow(IntPtr ptrWindow) {
			SDL2.SDL.SDL_RaiseWindow(ptrWindow);
		}
		public void Raise() {
			SdlCallUtil.ThrowIfSdlActionFails(TryRaiseWindow, this.GetPointer());
		}
		#endregion Raise
	}

	partial class SdlWindow {
		internal void HandleLoopEvent(SDL2.SDL.SDL_WindowEvent @event) {
			var eType = @event.windowEvent;
			if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE == eType) {
				this.HandleClose(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER == eType) {
				this.HandleEnter(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED == eType) {
				this.HandleExposed(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED == eType) {
				this.HandleFocusGained(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST == eType) {
				this.HandleFocusLost(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN == eType) {
				this.HandleHidden(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE == eType) {
				this.HandleLeave(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED == eType) {
				this.HandleMaximized(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED == eType) {
				this.HandleMinimized(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED == eType) {
				this.HandleMoved(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_NONE == eType) {
				return;
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED == eType) {
				this.HandleResized(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED == eType) {
				this.HandleRestored(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN == eType) {
				this.HandleShown(@event);
			}
			else if (SDL2.SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED == eType) {
				this.HandleSizeChanged(@event);
			}

			return;
		}
	}

	partial class SdlWindow {
		#region Close
		public void Close() {
			this.RaiseBeforeClose();
			this.Dispose();
		}

		private void HandleClose(SDL2.SDL.SDL_WindowEvent @event) {
			this.Sdl?.Registers?.Windows?.Remove(this);
			this.RaiseAfterClose();
		}

		/// <summary>
		/// the window manager requests that the window be closed
		/// </summary>
		public event EventHandler AfterClose;
		private void RaiseAfterClose() => this.AfterClose?.Invoke(this, EventArgs.Empty);

		public event EventHandler BeforeClose;
		private void RaiseBeforeClose() => this.BeforeClose?.Invoke(this, EventArgs.Empty);
		#endregion Close

		#region Enter
		private void HandleEnter(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterEnter();
		}

		/// <summary>
		/// window has gained mouse focus
		/// </summary>
		public event EventHandler AfterEnter;
		private void RaiseAfterEnter() => this.AfterEnter?.Invoke(this, EventArgs.Empty);
		#endregion Enter

		#region Expose
		private void HandleExposed(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterExpose();
		}

		/// <summary>
		/// window has been exposed and should be redrawn
		/// </summary>
		public event EventHandler AfterExpose;
		private void RaiseAfterExpose() => this.AfterExpose?.Invoke(this, EventArgs.Empty);
		#endregion Expose

		#region Focus
		private void HandleFocusGained(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterFocusGained();
		}

		/// <summary>
		/// window has gained keyboard focus
		/// </summary>
		public event EventHandler AfterFocusGained;
		private void RaiseAfterFocusGained() => this.AfterFocusGained?.Invoke(this, EventArgs.Empty);

		private void HandleFocusLost(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterFocusLost();
		}

		/// <summary>
		/// window has lost keyboard focus
		/// </summary>
		public event EventHandler AfterFocusLost;
		private void RaiseAfterFocusLost() => this.AfterFocusLost?.Invoke(this, EventArgs.Empty);
		#endregion Focus

		#region Hidden
		private void HandleHidden(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterHidden();
		}

		/// <summary>
		/// window has been hidden
		/// </summary>
		public event EventHandler AfterHidden;
		private void RaiseAfterHidden() => this.AfterHidden?.Invoke(this, EventArgs.Empty);
		#endregion Hidden

		#region Leave
		public void HandleLeave(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterLeave();
		}

		/// <summary>
		/// window has lost mouse focus
		/// </summary>
		public event EventHandler AfterLeave;
		private void RaiseAfterLeave() => this.AfterLeave?.Invoke(this, EventArgs.Empty);
		#endregion Leave

		#region SizeState
		private SdlWindowSizeState sizeState;
		public SdlWindowSizeState SizeState => this.sizeState;

		private static void TryMinimize(IntPtr ptrWindow) {
			SDL2.SDL.SDL_MinimizeWindow(ptrWindow);
		}

		public void Minimize() {
			this.RaiseBeforeMinimize();
			SdlCallUtil.ThrowIfSdlActionFails(TryMinimize, this.GetPointer());
		}

		private void HandleMinimized(SDL2.SDL.SDL_WindowEvent @event) {
			this.sizeState = SdlWindowSizeState.Minimized;
			this.RaiseAfterMinimize();
		}

		public event EventHandler BeforeMinimize;
		private void RaiseBeforeMinimize() => this.BeforeMinimize?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterMinimize;
		private void RaiseAfterMinimize() => this.AfterMinimize?.Invoke(this, EventArgs.Empty);

		private static void TryMaximize(IntPtr ptrWindow) {
			SDL2.SDL.SDL_MaximizeWindow(ptrWindow);
		}

		public void Maximize() {
			this.RaiseBeforeMaximize();
			SdlCallUtil.ThrowIfSdlActionFails(TryMaximize, this.GetPointer());
		}

		private void HandleMaximized(SDL2.SDL.SDL_WindowEvent @event) {
			this.sizeState = SdlWindowSizeState.Maximized;
			this.RaiseAfterMaximize();
		}

		public event EventHandler BeforeMaximize;
		private void RaiseBeforeMaximize() => this.BeforeMaximize?.Invoke(this, EventArgs.Empty);

		/// <summary>
		/// window has been minimized
		/// </summary>
		public event EventHandler AfterMaximize;
		private void RaiseAfterMaximize() => this.AfterMaximize?.Invoke(this, EventArgs.Empty);

		private static void TryRestore(IntPtr ptrWindow) {
			SDL2.SDL.SDL_RestoreWindow(ptrWindow);
		}
		public void Restore() {
			this.RaiseBeforeRestore();
			SdlCallUtil.ThrowIfSdlActionFails(TryRestore, this.GetPointer());
		}

		private void HandleRestored(SDL2.SDL.SDL_WindowEvent @event) {
			this.RaiseAfterRestore();
		}

		public event EventHandler BeforeRestore;
		private void RaiseBeforeRestore() => this.BeforeRestore?.Invoke(this, EventArgs.Empty);

		public event EventHandler AfterRestore;
		private void RaiseAfterRestore() => this.AfterRestore?.Invoke(this, EventArgs.Empty);
		#endregion SizeState
	}
}
