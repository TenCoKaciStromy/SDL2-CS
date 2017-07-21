using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public interface IChildControlCollection : ICollection<IChildControl> {
		bool HasChildren { get; }

		void AddBefore(IChildControl child, IChildControl nextChild);
		void AddAfter(IChildControl child, IChildControl prevChild);
	}
}
