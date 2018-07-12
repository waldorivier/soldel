
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace mupeModel {
    
    public partial class pe_ip : INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private int _no_ip;
        private ISet<pe_libl> _pe_libl_list;
        private IList<pe_muta> _pe_muta_list;

        partial void OnCreated();

        public override bool Equals(object obj) {
            pe_ip toCompare = obj as pe_ip;
            if (toCompare == null) {
                return false;
            }

            if (!Object.Equals(this._no_ip, toCompare._no_ip))
                return false;
   
            return true;
        }

        public override int GetHashCode() {
            int hashCode = 13;
            hashCode = (hashCode * 7) + _no_ip.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// There are no comments for pe_gmes constructor in the schema.
        /// </summary>
        public pe_ip() {
            this._no_ip = 0;
            this._pe_libl_list = new HashSet<pe_libl>();
            this._pe_muta_list = new List<pe_muta>();
            OnCreated();
        }
        
        /// <summary>
        /// There are no comments for pe_grmu_id in the schema.
        /// </summary>
        public virtual int no_ip {
            get {
                return this._no_ip;
            }
            set {
                if (this._no_ip != value) {
                    this.SendPropertyChanging();
                    this._no_ip = value;
                    this.SendPropertyChanged("no_ip");
                }
            }
        }
        public virtual ISet<pe_libl> pe_libl_list {
            get {
                return this._pe_libl_list;
            }
            set {
                this._pe_libl_list = value;
            }
        }
        public virtual IList<pe_muta> pe_muta_list {
            get {
                return this._pe_muta_list;
            }
            set {
                this._pe_muta_list = value;
            }
        }

        public virtual event PropertyChangingEventHandler PropertyChanging;
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging() {
            var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) {
            var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName) {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void add_muta(pe_muta muta) {
            muta.pe_ip = this;
            muta.no_ip = no_ip;
            pe_muta_list.Add(muta);
        }

        public virtual void add_libl(pe_libl libl) {
            libl.pe_ip = this;
            libl.no_ip = no_ip;
            pe_libl_list.Add(libl);
        }
    }
}
