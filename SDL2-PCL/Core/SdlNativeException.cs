using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Native;

namespace Allodium.SDL2.Core {
	public sealed class SdlNativeException : SdlException {
		public SdlNativeException() : base() { }
		public SdlNativeException(string message) : base(message) { }
		public SdlNativeException(string message, Exception innerException) : base(message, innerException) { }

		public static SdlNativeException TryCreateFromLastSdlError() => TryCreateFromLastSdlError(prefix: null);
		public static SdlNativeException TryCreateFromLastSdlError(string prefix) {
			var error = SDL.SDL_GetError();
			if (object.ReferenceEquals(error, null)) { return null; }

			var errMsg = $"{prefix}{error}";
			return new SdlNativeException(errMsg);
		}
		public static SdlNativeException CreateFromLastSdlError() => CreateFromLastSdlError(prefix: null);
		public static SdlNativeException CreateFromLastSdlError(string prefix) {
			var result = TryCreateFromLastSdlError();
			if (object.ReferenceEquals(result, null)) {
				throw new InvalidOperationException("There is not last SDL error.");
			}

			return result;
		}
	}
}
