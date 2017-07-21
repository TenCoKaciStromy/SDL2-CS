using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;

namespace UiQuick {
	public interface IRenderContext {
		SdlRenderer Renderer { get; }
	}
}
