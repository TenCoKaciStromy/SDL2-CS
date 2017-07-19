using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2
{
	public sealed class InvalidSdlObjectException : SdlException {
		public InvalidSdlObjectException() : base("Target SDL object is not valid.") { }
		public InvalidSdlObjectException(string message) : base(message) { }
		public InvalidSdlObjectException(string message, Exception innerException) : base(message, innerException) { }
	}
}
