using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UiQuick.Drawing {
	public sealed class RenderableCollection : IRenderableCollection, IRenderable {
		private readonly LinkedList<IRenderable> items = new LinkedList<IRenderable>();
		public int Count => this.items.Count;
		public bool IsReadOnly => false;

		#region IRenderable
		public void Render(IRenderContext context) {
			if (context is null) { throw new ArgumentNullException(nameof(context)); }

			foreach (var item in items) {
				item.Render(context);
			}
		}
		#endregion IRenderable

		#region IRenderableCollection
		public void Add(IRenderable item) {
			if (item is null) { return; }

			this.items.AddLast(item);
		}
		public void Clear() {
			this.items.Clear();
		}
		public bool Contains(IRenderable item) {
			return this.items.Contains(item);
		}
		public void CopyTo(IRenderable[] array, int arrayIndex) {
			this.items.CopyTo(array, arrayIndex);
		}
		public bool Remove(IRenderable item) {
			return this.items.Remove(item);
		}

		public IEnumerator<IRenderable> GetEnumerator() => this.items.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		#endregion IRenderableCollection
	}
}
