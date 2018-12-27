using System.Collections.Generic;

namespace HappyNomad.Outlining {

	/// <summary>Provides utility methods for manipulating tree structures.</summary>
	public static class OutliningUtil {
		#region Tree Manipulation Methods

		/// <summary>Inserts a new node into a tree structure</summary>
		/// <param name="newItem"></param>
		/// <param name="relativePos"></param>
		public static void Insert<T>( T newItem, RelativePosition<T> relativePos ) {
			IList<T> subItems =
			  (IList<T>)ReflectionUtil.NavigateToManySide<T>( relativePos.parent, relativePos.subItemsPropName );
			int newIndex = -1;

			if ( relativePos.command == OutliningCommands.NewSiblingBefore )
				newIndex = MinimumIndex<T>( subItems, relativePos.insertRelativeTo );
			else if ( relativePos.command == OutliningCommands.NewSiblingAfter )
				newIndex = MaximumIndex<T>( subItems, relativePos.insertRelativeTo ) + 1;
				//((Topic)parent).SubTopics.Insert( newIndex, (Topic)newItem );
			else if ( relativePos.command == OutliningCommands.NewChild ) {
				if ( relativePos.childIndex >= 0 ) newIndex = relativePos.childIndex;
				else newIndex = subItems.Count; // Insert the new item at the end of the sub-items list
			} else if ( relativePos.command == OutliningCommands.NewParent ) {
				//Insert the new item at the same position where the first selected item was located:
				newIndex = MinimumIndex( subItems, relativePos.insertRelativeTo );
				IList<T> newSubItems =
				  (IList<T>)ReflectionUtil.NavigateToManySide<T>( newItem, relativePos.subItemsPropName );
				foreach ( T item in relativePos.insertRelativeTo ) { //loop thru selected items:
					subItems.Remove( item ); //remove item from parent's sub-topics list
					newSubItems.Add( item ); //add it as child of the new item
				}
			}
			subItems.Insert( newIndex, newItem );
		}

		private static int MinimumIndex<T>( IList<T> parentList, IList<T> subItems ) {
			int result = parentList.Count;
			foreach ( T item in subItems ) {
				int index = parentList.IndexOf( item );
				if ( index < result ) result = index;
			}
			return result;
		}

		private static int MaximumIndex<T>( IList<T> parentList, IList<T> subItems ) {
			int result = 0;
			foreach ( T item in subItems ) {
				int index = parentList.IndexOf( item );
				if ( index > result ) result = index;
			}
			return result;
		}

		#endregion

		#region 'GenerateUniqueName' Method

		public static string GenerateUniqueName<T>(string newItemType , string nameProperty, ICollection<T> parentCollection) {
			string result = null;
			bool isNameUnique = false;

			for ( int i = 0; !isNameUnique; i++ ) { //find a unique name to use
				result = (i == 0) ? "New "+newItemType : "New "+newItemType+" (" + i + ")";
				isNameUnique = true;
				foreach ( T existingItem in parentCollection )
					if ( result.Equals(ReflectionUtil.NavigateToOneSide(existingItem, nameProperty)) )
						isNameUnique = false;
			}
			return result;
		}

		#endregion
	}
}
