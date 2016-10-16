using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public sealed class SdlRenderer : SdlObject {
		public SdlRenderer(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL.SDL_DestroyRenderer(this.handle);
			return true;
		}
	}
}
