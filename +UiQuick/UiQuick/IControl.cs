using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;

namespace UiQuick {
	public interface IControl {
		void Render(IRenderContext context);
		event EventHandler<RenderEventArgs> BeforeRender;
		event EventHandler<RenderEventArgs> AfterRender;
	}
}
