using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2
{
	[Flags]
	public enum SdlWindowCreationFlags : uint {
		Unkonwn = 0x0,
		/****************************************/
		Fullscreen = 0x00000001,
		OpenGL = 0x00000002,
		Shown = 0x00000004,
		Hidden = 0x00000008,
		Borderless = 0x00000010,
		Resizable = 0x00000020,
		Minimized = 0x00000040,
		Maximized = 0x00000080,
		InputGrabbed = 0x00000100,
		InputFocux = 0x00000200,
		MouseFocus = 0x00000400,
		FullscreenDesktop = (Fullscreen | 0x00001000),
		Foreign = 0x00000800,
		AllowHighDpi = 0x00002000,  /* Only available in 2.0.1 */
		MouseCapture = 0x00004000,  /* Only available in 2.0.4 */
	}
}
