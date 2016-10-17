using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public sealed partial class SdlWindow : SdlObject {
		public SdlWindow(IntPtr validHandle, bool ownsHandle, string title, SdlVector position, SdlVector size) : base(validHandle, ownsHandle) {
			this.title = title;
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
		#endregion Title

		#region Position
		private SdlVector position;
		public SdlVector Position {
			get { return this.position; }
			set {
				SDL.SDL_SetWindowPosition(this, value.X, value.Y);
				this.position = value;
			}
		}
		#endregion Position
	}
}