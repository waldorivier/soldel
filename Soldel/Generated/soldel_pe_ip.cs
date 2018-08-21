namespace mupeModel {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;

    public class pe_ip:INotifyPropertyChanging, INotifyPropertyChanged, IValueConverter {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

        private int _no_ip = 0;
        private string _nomf_ip = @"";
        private string _abgregf_ip = @"";

        private ISet<pe_libl> _pe_libl_list = new HashSet<pe_libl>();
        private IList<pe_muta> _pe_muta_list = new List<pe_muta>();
        private IList<pe_cfgt> _pe_cfgt_list = new List<pe_cfgt>();
        private IList<pe_grmu> _pe_grmu_list = new List<pe_grmu>();
               
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual void add_grmu(pe_grmu grmu) {
            grmu.pe_ip = this;
            grmu.no_ip = no_ip;
            this.pe_grmu_list.Add(grmu);
        }

        public virtual void add_libl(pe_libl libl) {
            libl.pe_ip = this;
            libl.no_ip = this.no_ip;
            this.pe_libl_list.Add(libl);
        }

        public virtual void add_muta(pe_muta muta) {
            muta.pe_ip = this;
            muta.no_ip = this.no_ip;
            this.pe_muta_list.Add(muta);
        }

        public override bool Equals(object obj) {
            pe_ip _ip = obj as pe_ip;
            if(_ip == null) {
                return false;
            }
            if(!object.Equals(this._no_ip,_ip._no_ip)) {
                return false;
            }
            return true;
        }

        public override int GetHashCode() {
            int num = 13;
            return ((num * 7) + this._no_ip.GetHashCode());
        }

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
        public virtual string nomf_ip {
            get =>
                this._nomf_ip;
            set {
                if(this._nomf_ip != value) {
                    this.SendPropertyChanging();
                    this._nomf_ip = value;
                    this.SendPropertyChanged("nomf_ip");
                }
            }
        }
        public virtual string abregf_ip {
            get =>
                this._abgregf_ip;
            set {
                if(this._abgregf_ip != value) {
                    this.SendPropertyChanging();
                    this._abgregf_ip = value;
                    this.SendPropertyChanged("abregf_ip");
                }
            }
        }

        public virtual ISet<pe_libl> pe_libl_list {
            get => this._pe_libl_list;
            set => this._pe_libl_list = value;
        }

        public virtual IList<pe_muta> pe_muta_list {
            get => this._pe_muta_list;
            set => this._pe_muta_list = value;
        }

        public virtual IList<pe_cfgt> pe_cfgt_list {
            get => this._pe_cfgt_list;
            set => this._pe_cfgt_list = value;
        }

        public virtual IList<pe_grmu> pe_grmu_list {
            get => this._pe_grmu_list;
            set => this._pe_grmu_list = value;
        }

        object IValueConverter.Convert(object value,Type targetType,object parameter,CultureInfo culture) {
            return null;
        }

        object IValueConverter.ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
