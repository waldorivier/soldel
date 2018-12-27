using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using NHibernate.Engine;
using NHibernate.Collection.Generic;
using NHibernate.Persister.Collection;
using System.ComponentModel;

namespace NHibernate.Collection.Observable {

	[Serializable, System.Diagnostics.DebuggerTypeProxy( typeof(NHibernate.DebugHelpers.CollectionProxy<>) )]
	public class PersistentObservableBag<T> : PersistentGenericBag<T>, INotifyCollectionChanged {

        private NotifyCollectionChangedEventHandler _collectionChanged;
        private PropertyChangedEventHandler _propertyChanged;

        public PersistentObservableBag(ISessionImplementor sessionImplementor)
            : base(sessionImplementor) {
        }

        public PersistentObservableBag(ISessionImplementor sessionImplementor, ICollection<T> coll)
            : base(sessionImplementor, coll) {
            CaptureEventHandlers(coll);
        }

        public PersistentObservableBag() {
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged {
            add {
                Initialize(false);
                _collectionChanged += value;
            }
            remove { _collectionChanged -= value; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged {
            add {
                Initialize(false);
                _propertyChanged += value;
            }
            remove { _propertyChanged += value; }
        }

        #endregion

        public override void BeforeInitialize(ICollectionPersister persister, int anticipatedSize) {
            base.BeforeInitialize(persister, anticipatedSize);
            CaptureEventHandlers(InternalBag);
        }

        private void CaptureEventHandlers(ICollection<T> coll) {
            var notificableCollection = coll as INotifyCollectionChanged;
            var propertyNotificableColl = coll as INotifyPropertyChanged;

            if (notificableCollection != null)
                notificableCollection.CollectionChanged += OnCollectionChanged;

            if (propertyNotificableColl != null)
                propertyNotificableColl.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            PropertyChangedEventHandler changed = _propertyChanged;
            if (changed != null) changed(this, e);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            NotifyCollectionChangedEventHandler changed = _collectionChanged;
            if (changed != null) changed(this, e);
        }
    }
}
