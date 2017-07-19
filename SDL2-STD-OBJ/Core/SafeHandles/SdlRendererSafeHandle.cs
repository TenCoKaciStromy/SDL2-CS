using System;
using System.Collections.Generic;
using System.Text;

using static SDL2.SDL;

namespace ObjectiveSdl2.Core.SafeHandles
{
	public sealed class SdlRendererSafeHandle : SdlSafeHandle {
		public SdlRendererSafeHandle(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL_DestroyRenderer(this.handle);
			return true;
		}
	}
}
