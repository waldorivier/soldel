using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using NHibernate.Engine;
using NHibernate.Collection.Generic;

namespace NHibernate.Collection.Observable {
	/// <summary>
	/// A persistent wrapper for a generic bag collection that fires events when
	/// item(s) have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the bag</typeparam>
	/// <author>Adrian Alexander</author>
	public class OldPersistentObservableBag<T> : PersistentGenericBag<T>, IList<T>, System.Collections.IList, INotifyCollectionChanged {
		public OldPersistentObservableBag( ISessionImplementor session ) : base( session ) { }
		public OldPersistentObservableBag( ISessionImplementor session, ObservableCollection<T> list ) : base( session, list ) { }

		public new void Add( T item ) {
			base.Add( item );
			OnCollectionChanged( NotifyCollectionChangedAction.Add, item, Count - 1 );
		}

		public new void Insert( int index, T item ) {
			base.Insert( index, item );
			OnCollectionChanged( NotifyCollectionChangedAction.Add, item, index );
		}

		public new bool Remove( T item ) {
			int index = IndexOf( item );
			bool isChanged = base.Remove( item );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Remove, item, index );
			return isChanged;
		}

		public new void RemoveAt( int index ) {
			T item = this[index];
			base.RemoveAt( index );
			OnCollectionChanged( NotifyCollectionChangedAction.Remove, item, index );
		}

		public new void Clear() {
			base.Clear();
			OnCollectionChanged( NotifyCollectionChangedAction.Reset, null, -1 );
		}

		#region INotifyCollectionChanged Members

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Raises the <see cref="CollectionChanged"/> event to indicate that item(s)
		/// have been added to, or removed from, this collection.
		/// </summary>
		protected virtual void OnCollectionChanged( NotifyCollectionChangedAction action, object changedItem, int index ) {
			if ( CollectionChanged != null )
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( action, changedItem, index ) );
		}

		#endregion
	}
}
