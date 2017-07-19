using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;

namespace ObjectiveSdl2.Drawing {
	public sealed partial class SdlSurface : SdlObject {
		public SdlSurface(SafeHandle validHandle) : base(validHandle) { }
		public SdlSurface(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => throw new NotImplementedException();
	}

	partial class SdlSurface {
		#region BlitFrom
		public void BlitFrom(SdlSurface source, SdlRect sourceRect, SdlRect destinationRect) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitSurface(ptrSource, sourceRect, this.GetValidHandle(), destinationRect);
		}
		public void BlitFrom(SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitSurface(ptrSource, sourceRect, this.GetValidHandle());
		}
		public void BlitFrom(SdlSurface source) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitSurface(ptrSource, this.GetValidHandle());
		}
		#endregion BlitFrom

		#region BlitTo
		public void BlitTo(SdlRect sourceRect, SdlSurface destination, SdlRect destinationRect) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitSurface(this.GetValidHandle(), sourceRect, ptrDestination, destinationRect);
		}
		public void BlitTo(SdlRect sourceRect, SdlSurface destination) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitSurface(this.GetValidHandle(), sourceRect, ptrDestination);
		}
		public void BlitTo(SdlSurface destination, SdlRect destinationRect) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitSurface(this.GetValidHandle(), ptrDestination, destinationRect);
		}
		public void BlitTo(SdlSurface destination) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitSurface(this.GetValidHandle(), ptrDestination);
		}
		#endregion BlitTo

		#region BlitScaledFrom
		public void BlitScaledFrom(SdlSurface source, SdlRect sourceRect, SdlRect destinationRect) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitScaled(ptrSource, sourceRect, this.GetValidHandle(), destinationRect);
		}
		public void BlitScaledFrom(SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitScaled(ptrSource, sourceRect, this.GetValidHandle());
		}
		public void BlitScaledFrom(SdlSurface source) {
			var ptrSource = source.GetValidHandle();
			this.Sdl.BlitScaled(ptrSource, this.GetValidHandle());
		}
		#endregion BlitScaledFrom

		#region BlitScaledTo
		public void BlitScaledTo(SdlRect sourceRect, SdlSurface destination, SdlRect destinationRect) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitScaled(this.GetValidHandle(), sourceRect, ptrDestination, destinationRect);
		}
		public void BlitScaledTo(SdlRect sourceRect, SdlSurface destination) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitScaled(this.GetValidHandle(), sourceRect, ptrDestination);
		}
		public void BlitScaledTo(SdlSurface destination, SdlRect destinationRect) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitScaled(this.GetValidHandle(), ptrDestination, destinationRect);
		}
		public void BlitScaledTo(SdlSurface destination) {
			var ptrDestination = destination.GetValidHandle();
			this.Sdl.BlitScaled(this.GetValidHandle(), ptrDestination);
		}
		#endregion BlitScaledTo
	}
}
