using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NHibernate.Engine;
using NHibernate.UserTypes;
using NHibernate.Persister.Collection;

namespace NHibernate.Collection.Observable {
	/// <summary>
	/// The NHibernate type for a generic list collection that fires events
	/// when item(s) have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the list</typeparam>
	/// <author>Adrian Alexander</author>
	public class ObservableListType<T> : IUserCollectionType {
		#region IUserCollectionType Members

		public bool Contains( object collection, object entity ) {
			return ((IList<T>)collection).Contains( (T)entity );
		}

		public IEnumerable GetElements( object collection ) {
			return (IEnumerable)collection;
		}

		public object IndexOf( object collection, object entity ) {
			return ((IList<T>)collection).IndexOf( (T)entity );
		}

		public object Instantiate( int anticipatedSize ) {
			return new ObservableCollection<T>();
		}

		public IPersistentCollection Instantiate( ISessionImplementor session, ICollectionPersister persister ) {
			return new PersistentObservableList<T>( session );
		}

		public object ReplaceElements( object original, object target, ICollectionPersister persister, object owner, IDictionary copyCache, ISessionImplementor session ) {
			IList<T> result = (IList<T>)target;
			result.Clear();
			foreach ( object item in ((IEnumerable)original) )
				result.Add( (T)item );
			return result;
		}

		public IPersistentCollection Wrap( ISessionImplementor session, object collection ) {
			return new PersistentObservableList<T>( session, (ObservableCollection<T>)collection );
		}

		#endregion
	}
}
