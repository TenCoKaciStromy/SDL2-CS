using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Core.SafeHandles;

namespace ObjectiveSdl2.Drawing
{
	public sealed partial class SdlTexture : SdlObject {
		public SdlTexture(SafeHandle validHandle) : base(validHandle) { }
		public SdlTexture(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => new SdlTextureSafeHandle(validHandle, ownsHandle);
	}

	partial class SdlTexture {
		#region QueryTexture
		private static SdlTextureInfo? TryQueryTexture(IntPtr ptrTexture) {
			var rslt = SDL2.SDL.SDL_QueryTexture(ptrTexture, out var format, out var access, out var width, out var height);
			if (0 == rslt) { return null; }

			return new SdlTextureInfo {
				Access = access,
				Format = format,
				Width = width,
				Height = height
			};
		}
		private SdlTextureInfo QueryTexture() {
			return SdlCallUtil.ThrowIfSdlFuncFails(TryQueryTexture, this.GetPointer()).Value;
		}
		#endregion QueryTexture

		#region Size
		private SdlVector size;
		public SdlVector Size => this.size;
		public SdlVector SizeRefresh() => this.size = this.QueryTexture().Size;
		#endregion Size
	}
}
