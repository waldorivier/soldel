using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mupeModel.Utils {

    class persistant_controller {

        private ISession session;

        public persistant_controller(ISession session) {
            session = session;
        }

        public void add_child(i_persistant parent, object child) {

            ITransaction transaction = null;

            try {
                transaction = session.BeginTransaction();

                parent.add_child(child);
                session.Save(parent);

                transaction.Commit();
                session.Refresh(parent);

            } catch(Exception ex) {
                if(transaction != null) {
                    if(transaction != null)
                        transaction.Rollback();

                }
            }
        }
    }
}
