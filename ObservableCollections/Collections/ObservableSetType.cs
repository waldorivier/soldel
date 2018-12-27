using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;

using NHibernate.Engine;
using NHibernate.UserTypes;
using NHibernate.Persister.Collection;

namespace NHibernate.Collection.Observable {
	/// <summary>
	/// The NHibernate type for a generic set collection that fires events
	/// when item(s) have been added to or removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of items in the set</typeparam>
	/// <author>Adrian Alexander</author>
	public class ObservableSetType<T> : IUserCollectionType {
		#region IUserCollectionType Members

		public bool Contains( object collection, object entity ) {
			return ((ISet<T>)collection).Contains( (T)entity );
		}

		public IEnumerable GetElements( object collection ) {
			return (IEnumerable)collection;
		}

		public object IndexOf( object collection, object entity ) {
			return -1;
		}

		public object Instantiate( int anticipatedSize ) {
			return new ObservableSet<T>();
		}

		public IPersistentCollection Instantiate( ISessionImplementor session, ICollectionPersister persister ) {
			return new PersistentObservableSet<T>( session );
		}

		public object ReplaceElements( object original, object target, ICollectionPersister persister, object owner, IDictionary copyCache, ISessionImplementor session ) {
			ISet<T> result = (ISet<T>)target;
			result.Clear();
			foreach ( object item in ((IEnumerable)original) )
				result.Add( (T)item );
			return result;
		}

		public IPersistentCollection Wrap( ISessionImplementor session, object collection ) {
			return new PersistentObservableSet<T>( session, (ObservableSet<T>)collection );
		}

		#endregion
	}
}
