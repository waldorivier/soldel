using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace mupeModel.Utils {
    class persistant_controller {
        private ISession _session;
        private TreeView _view;

        public persistant_controller(ISession session) {
            _session = session;
        }

        public ISession session { get => _session;}

        public void add_child(i_soldel parent, object child) {
            ITransaction transaction = null;
            if(parent.can_add_child(child)) {
                try {
                    transaction = _session.BeginTransaction();

                    parent.add_child(child);

                    if (parent.is_persistant())
                        _session.Save(parent);

                    transaction.Commit();

                    if (parent.is_persistant())
                        // _session.Refresh(parent);
                        ;
                      
                } catch(Exception ex) {
                    if(transaction != null) 
                        if(transaction != null)
                            transaction.Rollback();
                    
                    throw ex;
                }
            }
        }

        internal void delete(i_soldel parent, i_soldel child) {
            ITransaction transaction = null;
            if(child.can_remove_me()) {
                try {
                    transaction = _session.BeginTransaction();
                    child.remove_me();
                    _session.Delete(child);

                    transaction.Commit();
                    if (parent != null)
                        _session.Refresh(parent);

                } catch(Exception ex) {
                    if(transaction != null)
                        transaction.Rollback();

                    throw ex;
                }
            }
        }

        internal void update(object elem) {
            ITransaction transaction = null;
            try {
                transaction = _session.BeginTransaction();
                _session.Save(elem);
                transaction.Commit();
                _session.Refresh(elem);
            } catch(Exception ex) {
                if(transaction != null)
                    transaction.Rollback();

                throw ex;
            }
        }
    }
}
