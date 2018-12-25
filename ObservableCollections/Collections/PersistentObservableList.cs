using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using NHibernate.Engine;
using NHibernate.Collection.Generic;
using NHibernate.Persister.Collection;

namespace NHibernate.Collection.Observable {

	[Serializable, System.Diagnostics.DebuggerTypeProxy( typeof( NHibernate.DebugHelpers.CollectionProxy<> ) )]
	public class PersistentObservableList<T> : PersistentGenericList<T>, INotifyCollectionChanged {

		public PersistentObservableList( ISessionImplementor session )
			: base( session ) { }

		public PersistentObservableList( ISessionImplementor session, IList<T> coll )
			: base( session, coll ) {

			if ( coll != null )
				( (INotifyCollectionChanged)coll ).CollectionChanged += OnCollectionChanged;
		}

		public override void BeforeInitialize( ICollectionPersister persister, int anticipatedSize ) {
			base.BeforeInitialize( persister, anticipatedSize );
			( (INotifyCollectionChanged)this ).CollectionChanged += OnCollectionChanged;
		}

		#region INotifyCollectionChanged Members

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		protected void OnCollectionChanged( object sender, NotifyCollectionChangedEventArgs args ) {
			if ( CollectionChanged != null ) CollectionChanged( this, args );
		}

		#endregion
	}
}
