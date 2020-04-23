namespace mupeModel {
    using mupeModel.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Data;

    public class pe_muta:soldel, INotifyPropertyChanging, INotifyPropertyChanged, i_soldel {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
        private string _pe_muta_id = "";
        private string _u_version;
        private string _libf_muta;
        private string _libd_muta;
        private string _libe_muta;
        private string _libi_muta;
        private int _no_ip;
        private string _tyeven;
        private string _exttyeven;
        private string _user_cre;
        private DateTime? _dh_cre;
        private string _user_maj;
        private DateTime? _dh_maj;
        private int _muta_order;
        private string _liste_tycas;
        private string _type_grmu;
        private pe_ip _pe_ip;
        
        private IList<pe_gmmu> _pe_gmmu_list = new ObservableCollection<pe_gmmu>();
        private IList<pe_attr> _pe_attr_list = new ObservableCollection<pe_attr>();

        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual event PropertyChangingEventHandler PropertyChanging;

        public pe_muta() {
            this._libf_muta = this.libd_muta = this._libe_muta = this._libi_muta = "NAN";
            this._tyeven = "NAN";
        }
        
        protected virtual void SendPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if(propertyChanged != null) {
                propertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SendPropertyChanging() {
            PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
            if(propertyChanging != null) {
                propertyChanging(this,emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanging(string propertyName) {
            PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
            if(propertyChanging != null) {
                propertyChanging(this,new PropertyChangingEventArgs(propertyName));
            }
        }
            
        public virtual string pe_muta_id {
            get =>
                this._pe_muta_id;
            set {
                if(this._pe_muta_id != value) {
                    this.SendPropertyChanging();
                    this._pe_muta_id = value;
                    this.SendPropertyChanged("pe_muta_id");
                }
            }
        }

        public virtual string u_version {
            get =>
                this._u_version;
            set {
                if(this._u_version != value) {
                    this.SendPropertyChanging();
                    this._u_version = value;
                    this.SendPropertyChanged("u_version");
                }
            }
        }

        public virtual string libf_muta {
            get =>
                this._libf_muta;
            set {
                if(this._libf_muta != value) {
                    this.SendPropertyChanging();
                    this._libf_muta = value;
                    this.SendPropertyChanged("libf_muta");
                }
            }
        }

        public virtual string libd_muta {
            get =>
                this._libd_muta;
            set {
                if(this._libd_muta != value) {
                    this.SendPropertyChanging();
                    this._libd_muta = value;
                    this.SendPropertyChanged("libd_muta");
                }
            }
        }

        public virtual string libe_muta {
            get =>
                this._libe_muta;
            set {
                if(this._libe_muta != value) {
                    this.SendPropertyChanging();
                    this._libe_muta = value;
                    this.SendPropertyChanged("libe_muta");
                }
            }
        }

        public virtual string libi_muta {
            get =>
                this._libi_muta;
            set {
                if(this._libi_muta != value) {
                    this.SendPropertyChanging();
                    this._libi_muta = value;
                    this.SendPropertyChanged("libi_muta");
                }
            }
        }

        public virtual int no_ip {
            get =>
                this._no_ip;
            set {
                if(this._no_ip != value) {
                    this.SendPropertyChanging();
                    this._no_ip = value;
                    this.SendPropertyChanged("no_ip");
                }
            }
        }

        public virtual string tyeven {
            get =>
                this._tyeven;
            set {
                if(this._tyeven != value) {
                    this.SendPropertyChanging();
                    this._tyeven = value;
                    this.SendPropertyChanged("tyeven");
                }
            }
        }

        public virtual string exttyeven {
            get =>
                this._exttyeven;
            set {
                if(this._exttyeven != value) {
                    this.SendPropertyChanging();
                    this._exttyeven = value;
                    this.SendPropertyChanged("exttyeven");
                }
            }
        }

        public virtual string user_cre {
            get =>
                this._user_cre;
            set {
                if(this._user_cre != value) {
                    this.SendPropertyChanging();
                    this._user_cre = value;
                    this.SendPropertyChanged("user_cre");
                }
            }
        }

        public virtual DateTime? dh_cre {
            get =>
                this._dh_cre;
            set {
                DateTime? nullable = this._dh_cre;
                DateTime? nullable2 = value;
                if((nullable.HasValue == nullable2.HasValue) ? (nullable.HasValue ? (nullable.GetValueOrDefault() != nullable2.GetValueOrDefault()) : false) : true) {
                    this.SendPropertyChanging();
                    this._dh_cre = value;
                    this.SendPropertyChanged("dh_cre");
                }
            }
        }

        public virtual string user_maj {
            get =>
                this._user_maj;
            set {
                if(this._user_maj != value) {
                    this.SendPropertyChanging();
                    this._user_maj = value;
                    this.SendPropertyChanged("user_maj");
                }
            }
        }

        public virtual DateTime? dh_maj {
            get =>
                this._dh_maj;
            set {
                DateTime? nullable = this._dh_maj;
                DateTime? nullable2 = value;
                if((nullable.HasValue == nullable2.HasValue) ? (nullable.HasValue ? (nullable.GetValueOrDefault() != nullable2.GetValueOrDefault()) : false) : true) {
                    this.SendPropertyChanging();
                    this._dh_maj = value;
                    this.SendPropertyChanged("dh_maj");
                }
            }
        }

        public virtual int muta_order {
            get =>
                this._muta_order;
            set {
                if(this._muta_order != value) {
                    this.SendPropertyChanging();
                    this._muta_order = value;
                    this.SendPropertyChanged("muta_order");
                }
            }
        }

        public virtual string liste_tycas {
            get =>
                this._liste_tycas;
            set {
                if(this._liste_tycas != value) {
                    this.SendPropertyChanging();
                    this._liste_tycas = value;
                    this.SendPropertyChanged("liste_tycas");
                }
            }
        }

        public virtual string type_grmu {
            get =>
                this._type_grmu;
            set {
                if(this._type_grmu != value) {
                    this.SendPropertyChanging();
                    this._type_grmu = value;
                    this.SendPropertyChanged("type_grmu");
                }
            }
        }

        public virtual IList<pe_gmmu> pe_gmmu_list {
            get => this._pe_gmmu_list;
            set {
                this._pe_gmmu_list = value;
                this.SendPropertyChanged("pe_gmmu_list");
            }
        }

        public virtual IList<pe_attr> pe_attr_list {
            get => this._pe_attr_list;
            set {
                this._pe_attr_list = value;
                this.SendPropertyChanged("pe_attr_list");
            }
        }

        public virtual pe_ip pe_ip {
            get =>
                this._pe_ip;
            set {
                if(this._pe_ip != value) {
                    this.SendPropertyChanging();
                    this._pe_ip = value;
                    this.SendPropertyChanged("pe_ip");
                }
            }
        }
        
        #region I_SOLDEL

        bool i_soldel.is_persistant() {
            return true;
        }
        
        bool i_soldel.can_remove_me() {
            return pe_gmmu_list.Count == 0;
        }

        void i_soldel.remove_me() {
        }

        public virtual bool can_add_child(object child) {

            bool can_add = false;
            var attr = child as pe_attr;
            if(attr != null) {
                attr.pe_muta_id = this.pe_muta_id;
                can_add = !this.pe_attr_list.Contains(attr);
            }

            return can_add;
        }

        public virtual void add_child(object child) {

            var attr = child as pe_attr;
            if(attr != null) {
                attr.pe_muta = this;
                attr.pe_muta_id = this.pe_muta_id;
                this._pe_attr_list.Add(attr);
            }
        }
                
        public virtual pe_muta deep_copy(string muta_id,mupeModel.pe_ip ip) {

            i_soldel muta = shallow_copy(muta_id,ip);
            foreach(pe_attr _attr in pe_attr_list) {
                muta.add_child(_attr.shallow_copy(muta));
            }
            return muta;
        }

        public virtual i_soldel shallow_copy(string muta_id,mupeModel.pe_ip ip) {

            pe_muta copy = new pe_muta();
            copy_object.copy<pe_muta>(this,copy);

            copy.pe_muta_id = muta_id;
            copy.no_ip = ip.no_ip;
            copy.pe_ip = ip;

            return copy;
        }

        #endregion

        #region TREEVIEW

        public virtual new string tree_view_item_foreground {
            get { 
                if (this.pe_gmmu_list.Count > 1)
                    return "red";

                return "black";
            }
        }

        #endregion

        #region DATAGRID

        public static IList<string> columns_to_display = new List<string>() { "user_maj", "dh_maj", "tyeven", "exttyeven", "pe_muta_id",
                                                                              "muta_order", "libf_muta", "libd_muta", "libe_muta", "libi_muta", "type_grmu"};

        #endregion
    }
}
