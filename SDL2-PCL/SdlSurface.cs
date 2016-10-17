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
	public sealed partial class SdlSurface : SdlObject {
		public SdlSurface(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) {
			return new SdlSurfaceSafeHandle(validHandle, ownsHandle);
		}
	}

	partial class SdlSurface {
		#region BlitFrom
		public void BlitFrom(SdlSurface source, SdlRect? sourceRect, SdlRect? destinationRect) {
			this.Root.BlitSurface(source, sourceRect, this, destinationRect);
		}
		public void BlitFrom(SdlSurface source, SdlRect? sourceRect) => this.BlitFrom(source, sourceRect, null);
		public void BlitFrom(SdlSurface source) => this.BlitFrom(source, null, null);
		#endregion BlitFrom

		#region BlitTo
		public void BlitTo(SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) {
			this.Root.BlitSurface(this, sourceRect, destination, destinationRect);
		}
		public void BlitTo(SdlRect? sourceRect, SdlSurface destination) => this.BlitTo(sourceRect, destination, null);
		public void BlitTo(SdlSurface destination, SdlRect? destinationRect) => this.BlitTo(null, destination, destinationRect);
		public void BlitTo(SdlSurface destination) => this.BlitTo(null, destination, null);
		#endregion BlitTo

		#region BlitScaledFrom
		public void BlitScaledFrom(SdlSurface source, SdlRect? sourceRect, SdlRect? destinationRect) {
			this.Root.BlitScaled(source, sourceRect, this, destinationRect);
		}
		public void BlitScaledFrom(SdlSurface source, SdlRect? sourceRect) => this.BlitScaledFrom(source, sourceRect, null);
		public void BlitScaledFrom(SdlSurface source) => this.BlitScaledFrom(source, null, null);
		#endregion BlitScaledFrom

		#region BlitScaledTo
		public void BlitScaledTo(SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) {
			this.Root.BlitScaled(this, sourceRect, destination, destinationRect);
		}
		public void BlitScaledTo(SdlRect? sourceRect, SdlSurface destination) => this.BlitScaledTo(sourceRect, destination, null);
		public void BlitScaledTo(SdlSurface destination, SdlRect? destinationRect) => this.BlitScaledTo(null, destination, destinationRect);
		public void BlitScaledTo(SdlSurface destination) => this.BlitScaledTo(null, destination, null);
		#endregion BlitScaledTo
	}
}
