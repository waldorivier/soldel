namespace mupeModel {
    using mupeModel.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class pe_ip: soldel, INotifyPropertyChanging, INotifyPropertyChanged, i_soldel {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

        private int _no_ip = 0;
        private string _nomf_ip = @"";
        private string _abgregf_ip = @"";

        private ISet<pe_libl> _pe_libl_list = new HashSet<pe_libl>();
        private IList<pe_este> _pe_este_list = new ObservableCollection<pe_este>();
        private IList<pe_muta> _pe_muta_list = new ObservableCollection<pe_muta>();
        private IList<pe_cfgt> _pe_cfgt_list = new ObservableCollection<pe_cfgt>();
        private IList<pe_grmu> _pe_grmu_list = new ObservableCollection<pe_grmu>();

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

            // cette collection est un Set, soit un ensemble d'éléments uniques
            if (this.pe_libl_list.Contains(libl)) {
                var persistant_controller = new persistant_controller(hibernate_util.get_instance().get_current_session());
                persistant_controller.session.Merge(libl);
            } else {
                this.pe_libl_list.Add(libl);
            }
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
            if(!object.Equals(this.no_ip, _ip.no_ip)) {
                return false;
            }
            return true;
        }

        public override int GetHashCode() {
            int num = 13;
            return ((num * 7) + this.no_ip.GetHashCode());
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
            set {
                this.SendPropertyChanging();
                this._pe_libl_list = value;
                this.SendPropertyChanged("pe_libl_list");
            }
        }
        public virtual IList<pe_este> pe_este_list {
            get => this._pe_este_list;
            set {
                this.SendPropertyChanging();
                this._pe_este_list = value;
                this.SendPropertyChanged("pe_este_list");
            }
        }

        public virtual IList<pe_muta> pe_muta_list {
            get => this._pe_muta_list;
            set {
                this.SendPropertyChanging();
                this._pe_muta_list = value;
                this.SendPropertyChanged("pe_muta_list");
            }
        }

        public virtual IList<pe_cfgt> pe_cfgt_list {
            get => this._pe_cfgt_list;
            set {
                this.SendPropertyChanging();
                this._pe_cfgt_list = value;
                this.SendPropertyChanged("pe_cfgt_list");
            }
        }

        public virtual void paste_grmu(pe_grmu grmu) {
            var persistant_controller = new persistant_controller(hibernate_util.get_instance().get_current_session());
            var grmu_c = grmu.shallow_copy(hibernate_util.get_instance().generate_grmu_id(), this);
            persistant_controller.add_child(this, grmu_c);

            // copie de la configuration issue de la même ip; les mutations ne sont pas copiées mais
            // seule une référence est ajoutée
            if (grmu.pe_ip.Equals(this)) {
                foreach (var muta in grmu.pe_muta_list) {
                    pe_gmmu gmmu = new pe_gmmu(grmu_c, muta);
                    persistant_controller.update(gmmu);
                }
            } else {
                var muta_id = Int32.Parse(hibernate_util.get_instance().generate_muta_id());
                foreach (var muta in grmu.pe_muta_list) {
                    var muta_c = muta.deep_copy(muta_id.ToString(), grmu_c.pe_ip);
                    persistant_controller.update(muta_c);

                    pe_gmmu gmmu = new pe_gmmu(grmu_c, muta_c);
                    persistant_controller.update(gmmu);
                    muta_id++;
                }
            }
            persistant_controller.session.Refresh(grmu_c);
        }

        public virtual IList<pe_grmu> pe_grmu_list {
            get => this._pe_grmu_list;
            set {
                this.SendPropertyChanging();
                this._pe_grmu_list = value;
                this.SendPropertyChanged("pe_grmu_list");
            }
        }

        #region I_SOLDEL

        void i_soldel.add_child(object child) {
            var libl = child as pe_libl;
            if (libl != null) {

                // set collection; if items exists, replace it
                this.add_libl(libl);
            } else {
                var grmu = child as pe_grmu;
                if (grmu != null) {
                    grmu.pe_ip = this;
                    this._pe_grmu_list.Add(grmu);

                    // this.add_grmu(grmu)
                }
            }
        }
    
        bool i_soldel.can_add_child(object child) {
            return true;
        }

        bool i_soldel.can_remove_me() {
            throw new NotImplementedException();
        }

        void i_soldel.remove_me() {
            throw new NotImplementedException();
        }

        bool i_soldel.is_persistant() {
            return true;
        }

        #endregion
    }
}
