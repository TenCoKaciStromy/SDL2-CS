using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;

namespace UiQuick {
	public interface IControl {
		bool IsContainer { get; }

		void Render(SdlRenderer renderer);
	}
}
