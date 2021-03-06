﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2 {
	[Flags]
	public enum SdlRenderingFlags : uint {
		Unknown = 0x0,
		/****************************/
		Software = 0x00000001,
		Accelerated = 0x00000002,
		PresentVSync = 0x00000004,
		TargetTexture = 0x00000008
		/****************************/
	}
}
