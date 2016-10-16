using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2.Core {
	/// <summary>
	/// Base class for objects created by SDL native functions.
	/// </summary>
	public abstract class SdlObject : SafeHandle {
		public IntPtr Handle => this.handle;
		public IntPtr GetValidHandle() {
			var result = this.Handle;
			if (this.IsInvalid) {
				throw new InvalidSdlObjectException();
			}

			return result;
		}
		public override bool IsInvalid => IntPtr.Zero == this.handle;

		protected SdlObject(IntPtr validHandle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle) {
			this.handle = validHandle;
		}
	}
}
