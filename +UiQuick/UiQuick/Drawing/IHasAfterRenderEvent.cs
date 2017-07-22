using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick.Drawing {
	public interface IHasAfterRenderEvent {
		event RenderDelegate AfterRender;
	}
}
