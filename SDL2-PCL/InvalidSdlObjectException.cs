using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2 {
	public class InvalidSdlObjectException : SdlException {
		public InvalidSdlObjectException() : base("Target SDL object is not valid.") { }
		public InvalidSdlObjectException(string message) : base(message) { }
		public InvalidSdlObjectException(string message, Exception innerException) : base(message, innerException) { }
	}
}
