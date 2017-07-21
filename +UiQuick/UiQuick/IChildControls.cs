using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public interface IChildControls : ICollection<IChild> {
		bool HasChildren { get; }

		void AddBefore(IChild child, IChild nextChild);
		void AddAfter(IChild child, IChild prevChild);
	}
}
