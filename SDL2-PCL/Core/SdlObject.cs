using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2.Core {
	public interface ISdlObject : IDisposable {
		Sdl Root { get; }

		SafeHandle Handle { get; }
		IntPtr GetValidPointer();
		bool IsValid();
		bool IsInvalid();
	}

	/// <summary>
	/// Base class for objects created by SDL native functions.
	/// </summary>
	public abstract class SdlObject : ISdlObject {
		public Sdl Root => Sdl.Singleton;

		public SafeHandle Handle { get; }
		public IntPtr GetValidPointer() {
			var handle = this.Handle;
			if (object.ReferenceEquals(handle, null)) {
				throw new InvalidSdlObjectException();
			}

			if (handle.IsInvalid) {
				throw new InvalidSdlObjectException();
			}

			var result = handle.DangerousGetHandle();
			return result;
		}
		bool ISdlObject.IsValid() => !((ISdlObject)this).IsInvalid();
		bool ISdlObject.IsInvalid() {
			var handle = this.Handle;
			if (object.ReferenceEquals(handle, null)) { return true; }
			if (handle.IsInvalid) { return true; }

			return false;
		}

		protected SdlObject(IntPtr validHandle, bool ownsHandle) {
			var handle = this.CreateSdlSafeHandle(validHandle, ownsHandle);
			if (object.ReferenceEquals(handle, null)) {
				throw new InvalidOperationException("Can not create safe handle.");
			}

			this.Handle = handle;
		}
		protected abstract SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle);

		public virtual void Dispose() {
			this.Handle.Dispose();
		}
	}
}
