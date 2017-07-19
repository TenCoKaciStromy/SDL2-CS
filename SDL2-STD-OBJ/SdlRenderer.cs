using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Core.SafeHandles;

namespace ObjectiveSdl2
{
	public sealed partial class SdlRenderer : SdlObject {
		public SdlRenderer(SafeHandle validHandle) : base(validHandle) { }
		public SdlRenderer(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => new SdlRendererSafeHandle(validHandle, ownsHandle);
	}
}
