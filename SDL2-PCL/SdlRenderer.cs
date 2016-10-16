using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public sealed partial class SdlRenderer : SdlObject {
		public SdlRenderer(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override bool ReleaseHandle() {
			SDL.SDL_DestroyRenderer(this.handle);
			return true;
		}
	}

	partial class SdlRenderer {
		public void Clear() => this.Root.RenderClear(this);

		public SdlTexture CreateTexture(SdlSurface surface) => this.Root.CreateTextureFromSurface(this, surface);
		public SdlTexture CreateTextureFromBMP(string filePath) => this.Root.CreateTextureFromBMP(this, filePath);

		public void CopyFrom(SdlTexture texture) => this.Root.RenderCopy(this, texture);
		public void CopyFrom(SdlTexture texture, SdlRect? sourceRect, SdlRect? destinationRect) => this.Root.RenderCopy(this, texture, sourceRect, destinationRect);

		public void Present() => this.Root.RenderPresent(this);
	}
}
