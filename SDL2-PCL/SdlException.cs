using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Allodium.SDL2.Native;

namespace Allodium.SDL2 {
	public class SdlException : Exception {
		public SdlException() : base() { }
		public SdlException(string message) : base(message) { }
		public SdlException(string message, Exception innerException) : base(message, innerException) { }
	}
}
