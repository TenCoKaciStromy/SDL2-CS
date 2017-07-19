using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ObjectiveSdl2.Core {
	public interface ISdlObject : IDisposable {
		SafeHandle Handle { get; }
		IntPtr GetValidHandle();

		bool IsValid();
		bool IsInvalid();
	}
}
