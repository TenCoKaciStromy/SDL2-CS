using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2.Core {
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
