using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public interface IContainerControl : IControl {
		IChildControlCollection Children { get; }
	}
}
