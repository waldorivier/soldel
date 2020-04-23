namespace mupeModel {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;

    public class pe_este: soldel, INotifyPropertyChanging, INotifyPropertyChanged, i_soldel {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

        private int _no_ip = 0;
        private int _pe_este_id = 0;
        private string _no_nste = @"";
        private string _nom_este_1 = @"";
        private string _nom_este_2 = @"";
        private string _nom_este_3 = @"";
        private string _libf_este = @"";

        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual event PropertyChangingEventHandler PropertyChanging;

        public override bool Equals(object obj) {
            pe_este _este = obj as pe_este;
            if(_este == null) {
                return false;
            }
            if(!object.Equals(this.pe_este_id, _este.pe_este_id)) {
                return false;
            }
            return true;
        }

        public override int GetHashCode() {
            int num = 13;
            return ((num * 7) + this.pe_este_id.GetHashCode());
        }

        protected virtual void SendPropertyChanging() {
            var handler = this.PropertyChanging;
            if(handler != null)
                handler(this,emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) {
            var handler = this.PropertyChanging;
            if(handler != null)
                handler(this,new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName) {
            var handler = this.PropertyChanged;
            if(handler != null)
                handler(this,new PropertyChangedEventArgs(propertyName));
        }

        void i_soldel.add_child(object child) {
            throw new NotImplementedException();
        }

        bool i_soldel.can_add_child(object child) {
            throw new NotImplementedException();
        }

        bool i_soldel.can_remove_me() {
            throw new NotImplementedException();
        }

        void i_soldel.remove_me() {
            throw new NotImplementedException();
        }

        bool i_soldel.is_persistant() {
            throw new NotImplementedException();
        }

        public i_soldel shallow_copy() {
            throw new NotImplementedException();
        }

        public virtual int pe_este_id {
            get =>
                this._pe_este_id;
            set {
                if(this._pe_este_id != value) {
                    this.SendPropertyChanging();
                    this._pe_este_id = value;
                    this.SendPropertyChanged("pe_este_id");
                }
            }
        }
        public virtual string no_neste {
            get =>
                this._no_nste;
            set {
                if(this._no_nste != value) {
                    this.SendPropertyChanging();
                    this._no_nste = value;
                    this.SendPropertyChanged("no_nste");
                }
            }
        }
        public virtual string nom_este_1 {
            get =>
                this._nom_este_1;
            set {
                if(this._nom_este_1 != value) {
                    this.SendPropertyChanging();
                    this._nom_este_1 = value;
                    this.SendPropertyChanged("nom_este_1");
                }
            }
        }

        public virtual string nom_este_2 {
            get =>
                this._nom_este_2;
            set {
                if (this._nom_este_2 != value) {
                    this.SendPropertyChanging();
                    this._nom_este_2 = value;
                    this.SendPropertyChanged("nom_este_2");
                }
            }
        }

        public virtual string nom_este_3 {
            get =>
                this._nom_este_3;
            set {
                if (this._nom_este_3 != value) {
                    this.SendPropertyChanging();
                    this._nom_este_3 = value;
                    this.SendPropertyChanged("nom_este_3");
                }
            }
        }

        public virtual string libf_este {
            get =>
                this._libf_este;
            set {
                if (this._libf_este != value) {
                    this.SendPropertyChanging();
                    this._libf_este = value;
                    this.SendPropertyChanged("libf_este");
                }
            }
        }

    }
}
