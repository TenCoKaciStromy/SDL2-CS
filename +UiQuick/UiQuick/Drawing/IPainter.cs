using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;

namespace UiQuick.Drawing {
	public interface IPainter {
		void Paint(SdlRect bounds, IRenderContext context);
	}
}
