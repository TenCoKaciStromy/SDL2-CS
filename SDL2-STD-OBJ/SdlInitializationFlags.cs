using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2
{
	[Flags]
	public enum SdlInitializationFlags : uint {
		Unknown = 0x0,
		/***************************/
		Timer = 0x00000001,
		Audio = 0x00000010,
		Video = 0x00000020,
		Joystick = 0x00000200,
		Haptic = 0x00001000,
		GameController = 0x00002000,
		NoParachute = 0x00100000,
		/***************************/
		Everything = (Timer | Audio | Video | Joystick | Haptic | GameController)
	}
}
