using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;
using UiQuick.Drawing;

namespace UiQuick {
	public interface IControl : IRenderable, IHasBeforeRenderEvent, IHasAfterRenderEvent { }
}
