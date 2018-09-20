using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mupeModel.Utils {

    class persistant_controller {

        private ISession _session;

        public persistant_controller(ISession session) {
            _session = session;
        }

        public void add_child(i_persistant parent, object child) {

            ITransaction transaction = null;

            if(parent.can_add_child(child)) {
                try {
                    transaction = _session.BeginTransaction();

                    parent.add_child(child);
                    _session.Save(parent);

                    transaction.Commit();
                    _session.Refresh(parent);

                } catch(Exception ex) {
                    if(transaction != null) {
                        if(transaction != null)
                            transaction.Rollback();
                    }
                    MessageBox.Show(ex.Message);
                }
            }
        }

        internal void delete(i_persistant parent, i_persistant elem) {

            ITransaction transaction = null;

            if(elem.can_remove_me()) {
                try {

                    transaction = _session.BeginTransaction();
                    elem.remove_me();
                    _session.Delete(elem);

                    transaction.Commit();
                    _session.Refresh(parent);

                } catch(Exception ex) {
                    if(transaction != null) {
                        if(transaction != null)
                            transaction.Rollback();
                    }
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
