﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Core.SafeHandles;

namespace Allodium.SDL2 {
	public sealed partial class SdlCursor : SdlObject {
		public SdlCursor(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) {
			return new SdlCursorSafeHandle(validHandle, ownsHandle);
		}
	}

	partial class SdlCursor {
	}
}
