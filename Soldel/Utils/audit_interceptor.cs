namespace mupeModel.Utils {
    using NHibernate;
    using NHibernate.Type;
    using System;

    public class audit_interceptor:EmptyInterceptor {
        private int updates;
        private int creates;
        private int loads;
        private int deletes;

        public override void AfterTransactionCompletion(ITransaction tx) {
            if(tx != null) {
                if(tx.WasCommitted) {
                    Console.WriteLine(string.Concat(new object[] { "Creations: ",this.creates,", Updates: ",this.updates,", Loads: ",this.loads }));
                }
                this.updates = 0;
                this.creates = 0;
                this.loads = 0;
            }
        }

        public override void OnDelete(object entity,object id,object[] state,string[] propertyNames,IType[] types) {
            this.updates++;
        }

        public override bool OnFlushDirty(object entity,object id,object[] currentState,object[] previousState,string[] propertyNames,IType[] types) {
            bool flag = false;

            this.updates++;
            for(int i = 0;i < propertyNames.Length;i++) {
                if("dh_maj".Equals(propertyNames[i])) {
                    currentState[i] = DateTime.Now;
                    flag = true;
                } else if("user_maj".Equals(propertyNames[i])) {
                    currentState[i] = hibernate_util.get_instance().get_user();
                    flag = true;
                }
            }
            return flag;
        }

        public override bool OnLoad(object entity,object id,object[] state,string[] propertyNames,IType[] types) {
            this.loads++;
            return false;
        }

        public override bool OnSave(object entity,object id,object[] state,string[] propertyNames,IType[] types) {
            bool flag = false;
            this.creates++;
            for(int i = 0;i < propertyNames.Length;i++) {
                if("dh_cre".Equals(propertyNames[i])) {
                    state[i] = DateTime.Now;
                    flag = true;
                } else if("user_cre".Equals(propertyNames[i])) {
                    state[i] = hibernate_util.get_instance().get_user();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
