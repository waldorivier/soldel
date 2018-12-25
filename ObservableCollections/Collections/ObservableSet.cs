using System.Collections.Specialized;
using System.Collections.Generic;

namespace Iesi.Collections.Generic {
	/// <summary>
	/// A generic set collection that fires events when item(s)
	/// have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the set</typeparam>
	/// <author>Adrian Alexander</author>
	public class ObservableSet<T> : HashSet<T>, ISet<T>, INotifyCollectionChanged {
            
		public bool Add( T item ) {
			bool isChanged = base.Add( item );
			if ( isChanged ) {
				addOrder.Add( (T)item );
				OnCollectionChanged( NotifyCollectionChangedAction.Add, item );
			}
			return isChanged;
		}

		public bool Remove( T item ) {
			// WPF requires the list index to be passed back to itself:
			int index = IndexOf( item );
			bool isChanged = base.Remove( item );
			if ( isChanged ) {
				addOrder.Remove( (T)item );
				if ( CollectionChanged != null )
					CollectionChanged( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Remove, (object)item, index ) );
			}
			return isChanged;
		}

		public void Clear() {
			base.Clear();
			addOrder.Clear();
			OnCollectionChanged( NotifyCollectionChangedAction.Reset, null );
		}

		/// <summary>
		/// WPF requires the list index to be passed back to
		/// itself when removing an item from the collection.
		/// Sets have no indices, so they are hereby added.
		/// </summary>
		public int IndexOf( T item ) { return addOrder.IndexOf( item ); }
		private IList<T> addOrder = new List<T>();

		#region INotifyCollectionChanged Members

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Raises the <see cref="CollectionChanged"/> event to indicate that item(s)
		/// have been added to, or removed from, this collection.
		/// </summary>
		protected virtual void OnCollectionChanged( NotifyCollectionChangedAction action, object changedItem ) {
			if ( CollectionChanged != null )
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( action, changedItem ) );
		}

		#endregion
	}
}
