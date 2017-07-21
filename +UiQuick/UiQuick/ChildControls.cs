using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public sealed class ChildControls : IChildControls {
		private readonly LinkedList<IChild> children = new LinkedList<IChild>();
		public int Count => this.children.Count;
		public bool IsReadOnly => false;

		public bool HasChildren => 0 < this.children.Count;

		public void Add(IChild item) {
			if (item is null) { throw new ArgumentNullException(nameof(item)); }

			item.RemoveFromParent();
			this.children.AddLast(item);
		}
		public void AddBefore(IChild child, IChild nextChild) {
			if (child is null) { throw new ArgumentNullException(nameof(child)); }

			if (nextChild is null) {
				this.children.AddFirst(child);
				return;
			}

			LinkedListNode<IChild> node;
			while (!((node = this.children.Last) is null)) {
				if (object.ReferenceEquals(node.Value, nextChild)) {
					this.children.AddBefore(node, nextChild);
					return;
				}
			}

			this.children.AddFirst(child);
		}
		public void AddAfter(IChild child, IChild prevChild) {
			if (child is null) { throw new ArgumentNullException(nameof(child)); }

			if (prevChild is null) {
				this.children.AddLast(child);
				return;
			}

			LinkedListNode<IChild> node;
			while (!((node = this.children.Last) is null)) {
				if (object.ReferenceEquals(node.Value, prevChild)) {
					this.children.AddAfter(node, prevChild);
					return;
				}
			}

			this.children.AddLast(child);
		}

		public void Clear() {
			LinkedListNode<IChild> node;
			while (!((node = this.children.Last) is null)) {
				var child = node.Value;
				child?.RemoveFromParent();
			}
		}
		public bool Contains(IChild item) {
			return this.children.Contains(item);
		}
		public void CopyTo(IChild[] array, int arrayIndex) {
			this.children.CopyTo(array, arrayIndex);
		}
		public bool Remove(IChild item) {
			var rsltSelf = this.children.Remove(item);
			var rsltItem = item.RemoveFromParent(this);

			return rsltSelf || rsltSelf;
		}

		public IEnumerator<IChild> GetEnumerator() => this.children.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
