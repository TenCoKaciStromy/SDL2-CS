using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2
{
	public abstract class SdlException : Exception {
		public SdlException() : base() { }
		public SdlException(string message) : base(message) { }
		public SdlException(string message, Exception innerException) : base(message, innerException) { }
	}
}
