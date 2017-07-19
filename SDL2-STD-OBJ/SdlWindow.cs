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
			return SdlContext.Default.CreateWindow(title, position.X, position.Y, size.X, size.Y, creationFlags);
		}
	}

	partial class SdlWindow {
		#region Title
		private string title;
		public string Title {
			get => this.title;
			set {
				var okValue = value ?? string.Empty;
				Sdl.SetWindowTitle(this.GetValidHandle(), okValue);
				this.title = okValue;
			}
		}
		public string TitleRefresh() => this.title = Sdl.GetWindowTitle(this);
		#endregion Title

		#region Position
		private SdlVector position;
		public SdlVector Position {
			get => this.position;
			set {
				this.Sdl.SetWindowPosition(this.GetValidHandle(), value);
				this.position = value;
			}
		}
		public SdlVector PositionRefresh() => this.position = Sdl.GetWindowPosition(this.GetValidHandle());
		#endregion Position

		#region Size
		private SdlVector size;
		public SdlVector Size {
			get => this.size;
			set {
				this.Sdl.SetWindowSize(this.GetValidHandle(), value);
				this.size = value;
			}
		}
		public SdlVector SizeRefresh() => this.size = this.Sdl.GetWindowSize(this.GetValidHandle());
		#endregion Size
	}

	partial class SdlWindow {
		#region Renderer
		private SdlRenderer renderer;
		public SdlRenderer Renderer {
			get => this.renderer;
		}

		public SdlRenderer GetRenderer() {
			var result = this.renderer;
			if (!(result is null)) {
				if (result.IsInvalid()) {
					this.renderer = result = null;
				}
			}

			if (result is null) {
				this.renderer = result = this.Sdl.GetRenderer(this.GetValidHandle());
			}

			return result;
		}
		#endregion Renderer
	}
}
