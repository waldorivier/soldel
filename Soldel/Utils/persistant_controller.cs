using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mupeModel.Utils {

    class persistant_controller {

        private ISession session_;

        public persistant_controller(ISession session) {
            session_ = session;
        }

        public void add_child(i_persistant parent, object child) {

            ITransaction transaction = null;

            if(parent.can_add_child(child)) {
                try {
                    transaction = session_.BeginTransaction();

                    parent.add_child(child);
                    session_.Save(parent);

                    transaction.Commit();
                    session_.Refresh(parent);

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
