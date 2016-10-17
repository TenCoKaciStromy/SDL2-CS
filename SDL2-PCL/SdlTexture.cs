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
	public sealed partial class SdlTexture : SdlObject {
		public SdlTexture(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) {
			return new SdlTextureSafeHandle(validHandle, ownsHandle);
		}
	}

	partial class SdlTexture {
		public SdlVector Size { get; internal set; }
		public SdlVector QuerySize() {
			var result = this.Root.QueryTexture(this);
			this.Size = result;
			return result;
		}
	}
}
