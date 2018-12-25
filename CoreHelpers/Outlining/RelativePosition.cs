using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace HappyNomad.Outlining {

	public struct RelativePosition<T> {

		/// <summary>The outlining insertion command to use</summary>
		public ICommand command;

		/// <summary>Name of the subitems property</summary>
		public string subItemsPropName;

		/// <summary>Parent to the new item</summary>
		public T parent;

		/// <summary>Children of the parent, relative to
		///   which the new item will be positioned</summary>
		public IList<T> insertRelativeTo;

		/// <summary>Desired index when inserting a child</summary>
		public int childIndex;

		public RelativePosition( ICommand command, T parent, IList<T> insertRelativeTo ) {
			this.command = command; this.subItemsPropName = null; this.parent = parent;
			this.insertRelativeTo = insertRelativeTo; this.childIndex = -1;
		}

		public RelativePosition( ICommand command, string subItemsPropName, T parent, IList<T> insertRelativeTo ) {
			this.command = command; this.subItemsPropName = subItemsPropName; this.parent = parent;
			this.insertRelativeTo = insertRelativeTo; this.childIndex = -1;
		}
	}
}
