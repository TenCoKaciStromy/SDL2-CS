using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Core.SafeHandles;
using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public sealed partial class SdlWindow : SdlObject {
		public SdlWindow(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) {
		}
		public SdlWindow(IntPtr validHandle, bool ownsHandle, string title, SdlVector position, SdlVector size) : base(validHandle, ownsHandle) {
			this.title = title;
			this.position = position;
			this.size = size;
		}

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) {
			return new SdlWindowSafeHandle(validHandle, ownsHandle);
		}
	}

	partial class SdlWindow {
		public SdlRenderer CreateRenderer(SdlRenderingFlags renderingFlags) => this.Root.CreateRenderer(this, renderingFlags);
	}

	partial class SdlWindow {
		#region Title
		private string title;
		public string Title {
			get { return this.title; }
			set {
				var okValue = null != value ? value : string.Empty;
				this.Root.SetWindowTitle(this, okValue);
				this.title = okValue;
			}
		}

		public string RefreshTitle() {
			var ptr = this.GetValidPointer();
			var result = SDL.SDL_GetWindowTitle(ptr);
			this.title = result;
			return result;
		}
		#endregion Title

		#region Position
		private SdlVector position;
		public SdlVector Position {
			get { return this.position; }
			set {
				var ptr = this.GetValidPointer();
				SDL.SDL_SetWindowPosition(ptr, value.X, value.Y);
				this.position = value;
			}
		}

		public SdlVector RefreshPosition() {
			var ptr = this.GetValidPointer();
			int x, y;
			SDL.SDL_GetWindowPosition(ptr, out x, out y);
			var result = new SdlVector(x, y);
			this.position = result;
			return result;
		}
		#endregion Position

		#region Size
		private SdlVector size;
		public SdlVector Size {
			get { return this.size; }
			set {
				var ptr = this.GetValidPointer();
				SDL.SDL_SetWindowSize(ptr, value.X, value.Y);
				this.size = value;
			}
		}

		public SdlVector RefreshSize() {
			var ptr = this.GetValidPointer();
			int x, y;
			SDL.SDL_GetWindowSize(ptr, out x, out y);
			var result = new SdlVector(x, y);
			this.size = result;
			return result;
		}
		#endregion Size
	}
}