using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using NHibernate.Engine;
using NHibernate.Collection.Generic;
using Iesi.Collections;
using Iesi.Collections.Generic;

namespace NHibernate.Collection.Observable {
	/// <summary>
	/// A persistent wrapper for a generic set collection that fires events when
	/// item(s) have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the set</typeparam>
	/// <author>Adrian Alexander</author>
	public class OldPersistentObservableSet<T> : PersistentGenericSet<T>, ISet<T>, ISet, INotifyCollectionChanged {
		public OldPersistentObservableSet( ISessionImplementor session ) : base( session ) { }
		public OldPersistentObservableSet( ISessionImplementor session, ObservableSet<T> set ) : base( session, set ) { }
		
		#region ISet<T> Members

		public new bool Add( T item ) {
			bool isChanged = base.Add( (T)item );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Add, item );
			return isChanged;
		}

		void ICollection<T>.Add( T item ) {
			this.Add( (T)item ); // ignores boolean return value
		}

		public new bool AddAll( ICollection<T> items ) {
			bool isChanged = base.AddAll( (ICollection<T>)items );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Add, items );
			return isChanged;
		}

		public new bool Remove( T item ) {
			// WPF requires the list index to be passed back to itself:
			int index = ((ObservableSet<T>)internalSet).IndexOf( item );
			bool isChanged = base.Remove( (T)item );
			if ( isChanged && CollectionChanged != null )
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Remove, (object)item, index ) );
			return isChanged;
		}

		public new bool RemoveAll( ICollection<T> items ) {
			bool isChanged = base.RemoveAll( (ICollection<T>)items );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Remove, items );
			return isChanged;
		}

		public new bool RetainAll( ICollection<T> items ) {
			ISet<T> removeItems = Minus( (ISet<T>)new HashedSet<T>( items ) );
			if ( removeItems.IsEmpty ) return false;
			return this.RemoveAll( (ICollection<T>)removeItems );
		}

		#endregion

		#region ISet Members

		public new bool Add( object item ) {
			bool isChanged = base.Add( (object)item );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Add, item );
			return isChanged;
		}

		public new bool AddAll( ICollection items ) {
			bool isChanged = base.AddAll( (ICollection)items );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Add, items );
			return isChanged;
		}

		public new void Clear() {
			base.Clear();
			OnCollectionChanged( NotifyCollectionChangedAction.Reset, null );
		}

		public new bool Remove( object item ) {
			// WPF requires the list index to be passed back to itself:
			int index = ((ObservableSet<T>)internalSet).IndexOf( (T)item );
			bool isChanged = base.Remove( (object)item );
			if ( isChanged && CollectionChanged != null )
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Remove, (object)item, index ) );
			return isChanged;
		}

		public new bool RemoveAll( ICollection items ) {
			bool isChanged = base.RemoveAll( (ICollection)items );
			if ( isChanged ) OnCollectionChanged( NotifyCollectionChangedAction.Remove, items );
			return isChanged;
		}

		public new bool RetainAll( ICollection items ) {
			ISet removeItems = Minus( (ISet)new HashedSet( items ) );
			if ( removeItems.IsEmpty ) return false;
			return this.RemoveAll( (ICollection)removeItems );
		}

		#endregion
		
		#region INotifyCollectionChanged Members

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Raises the <see cref="CollectionChanged"/> event to indicate that item(s)
		/// have been added to, or removed from, this collection.
		/// </summary>
		protected virtual void OnCollectionChanged( NotifyCollectionChangedAction action, object changedItems ) {
			if ( CollectionChanged == null ) return;
			IList changedItemsList = changedItems as IList;
			if ( changedItemsList == null ) // error if item position hasn't been specified:
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( action, (object)changedItems ) );
			else
				CollectionChanged( this, new NotifyCollectionChangedEventArgs( action, (IList)changedItemsList ) );
		}

		#endregion
	}
}
