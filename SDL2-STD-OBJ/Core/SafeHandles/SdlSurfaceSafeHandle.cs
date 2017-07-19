using System;
using System.Collections.Generic;
using System.Text;

using static SDL2.SDL;

namespace ObjectiveSdl2.Core.SafeHandles
{
	public sealed class SdlSurfaceSafeHandle : SdlSafeHandle {
		public SdlSurfaceSafeHandle(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL_FreeSurface(this.handle);
			return true;
		}
	}
}
