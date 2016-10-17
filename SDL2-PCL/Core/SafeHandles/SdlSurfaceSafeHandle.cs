using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Native;

namespace Allodium.SDL2.Core.SafeHandles {
	public sealed class SdlSurfaceSafeHandle : SdlSafeHandle {
		public SdlSurfaceSafeHandle(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL.SDL_FreeSurface(this.handle);
			return true;
		}
	}
}
