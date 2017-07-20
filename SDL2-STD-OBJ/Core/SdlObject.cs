using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ObjectiveSdl2.Core
{
    public abstract class SdlObject : ISdlObject {
		protected SdlContext Sdl => SdlContext.Default;

		private readonly SafeHandle handle;
		SafeHandle ISdlObject.Handle => this.handle;

		protected IntPtr GetPointer() => this.handle.DangerousGetHandle();
		protected IntPtr GetValidHandle() {
			var result = this.handle;
			if (result is null || result.IsInvalid) {
				throw new InvalidSdlObjectException();
			}

			return handle.DangerousGetHandle();
		}
		IntPtr ISdlObject.GetValidHandle() => this.GetValidHandle();

		public bool IsValid() => throw new NotImplementedException();
		public bool IsInvalid() => throw new NotImplementedException();

		protected SdlObject(SafeHandle validHandle) {
			this.handle = validHandle ?? throw new ArgumentNullException(nameof(validHandle));
		}
		protected SdlObject(IntPtr validHandle, bool ownsHandle) {
			this.handle = this.CreateSdlSafeHandle(validHandle, ownsHandle);
		}

		protected abstract SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle);

		public void Dispose() {
			this.handle.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
