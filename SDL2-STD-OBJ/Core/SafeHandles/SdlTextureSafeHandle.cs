using System;
using System.Collections.Generic;
using System.Text;

using static SDL2.SDL;

namespace ObjectiveSdl2.Core.SafeHandles
{
	public sealed class SdlTextureSafeHandle : SdlSafeHandle {
		public SdlTextureSafeHandle(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL_DestroyTexture(this.handle);
			return true;
		}
	}
}
