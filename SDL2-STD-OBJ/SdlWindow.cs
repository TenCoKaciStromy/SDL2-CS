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
			return new SdlWindow(ptr, true);
		}
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
			SdlCallUtil.ThrowIfSdlActionFails(TryShowWindow, this.GetPointer());
		}
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
}
