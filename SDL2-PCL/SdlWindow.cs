using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public sealed class SdlWindow : SdlObject {
		public SdlWindow(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL.SDL_DestroyWindow(this.handle);
			return true;
		}
	}
}
