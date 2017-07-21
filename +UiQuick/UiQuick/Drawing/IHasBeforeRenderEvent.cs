using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick.Drawing {
	public interface IHasBeforeRenderEvent {
		event EventHandler<RenderEventArgs> BeforeRender;
	}
}
