using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ObjectiveSdl2.Core
{
	/// <summary>
	/// Base class for SafeHandle of SDL pointers.
	/// </summary>
	public abstract class SdlSafeHandle : SafeHandle {
		public override bool IsInvalid => IntPtr.Zero == this.handle;

		public SdlSafeHandle(IntPtr validHandle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle) {
			this.handle = validHandle;
		}
	}
}
