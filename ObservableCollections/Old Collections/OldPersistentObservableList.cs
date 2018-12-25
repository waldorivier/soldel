using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using NHibernate.Engine;
using NHibernate.Collection.Generic;

namespace NHibernate.Collection.Observable {
	/// <summary>
	/// A persistent wrapper for a generic list collection that fires events when
	/// item(s) have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the list</typeparam>
	/// <author>Adrian Alexander</author>
	public class OldPersistentObservableList<T> : PersistentGenericList<T>, IList<T>, IList, INotifyCollectionChanged {
		public OldPersistentObservableList( ISessionImplementor session ) : base( session ) { }
		public OldPersistentObservableList( ISessionImplementor session, ObservableCollection<T> list ) : base( session, list ) { }

		#region IList<T> Members

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

		#endregion

		#region IList Members

		//Calls to base methods in this region should have (object)item rather
		//than (T)item in their parameter lists, but the needed non-generic base
		//methods are explicitly implemented, therefore inaccessable from this
		//derived class. The generic versions are being used here instead.

		public int Add( object item ) {
			base.Add( (T)item );
			OnCollectionChanged( NotifyCollectionChangedAction.Add, item, Count - 1 );
			return IndexOf( (T)item );
		}

		public void Insert( int index, object item ) {
			base.Insert( index, (T)item );
			OnCollectionChanged( NotifyCollectionChangedAction.Add, item, index );
		}

		public void Remove( object item ) {
			int index = IndexOf( (T)item );
			bool isChanged = base.Remove( (T)item );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Remove, item, index );
		}

		#endregion

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
