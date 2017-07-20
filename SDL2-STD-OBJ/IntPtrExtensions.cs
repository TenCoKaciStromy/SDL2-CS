using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2 {
	public static class IntPtrExtensions {
		public static bool IsZero(this IntPtr self) => IntPtr.Zero == self;
	}
}
