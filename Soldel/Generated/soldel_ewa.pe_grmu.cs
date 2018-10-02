//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 18.03.2018 17:44:20
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using mupeModel.Utils;
using NHibernate.Criterion;

namespace mupeModel {

    /// <summary>
    /// There are no comments for pe_grmu, Soldel in the schema.
    /// </summary>
    public partial class pe_grmu : INotifyPropertyChanging, INotifyPropertyChanged, IValueConverter {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private string _pe_grmu_id;

        private string _u_version;

        private string _libf_grmu;

        private string _libd_grmu;

        private string _libe_grmu;

        private string _libi_grmu;

        private string _description;

        private int _no_ip;

        private string _type_grmu;

        private string _user_cre;

        private System.Nullable<System.DateTime> _dh_cre;

        private string _user_maj;

        private System.Nullable<System.DateTime> _dh_maj;

        private IList<pe_cfgd> _pe_cfgd_list;

        private IList<pe_gmmu> _pe_gmmu_list;

        private IList<pe_gmes> _pe_gmes_list;

        private IList<pe_muta> _pe_muta_list;

        private pe_ip _pe_ip;

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for pe_grmu constructor in the schema.
        /// </summary>
        public pe_grmu() {
            this._pe_grmu_id = @"";
            this._libf_grmu = this.libd_grmu = this._libe_grmu = this._libi_grmu = "libelle nouveau groupe de mutation(s)";
            this._pe_cfgd_list = new ObservableCollection<pe_cfgd>();
            this._pe_gmmu_list = new ObservableCollection<pe_gmmu>();
            this._pe_gmes_list = new ObservableCollection<pe_gmes>();
            this._pe_muta_list = new ObservableCollection<pe_muta>();
            OnCreated();
        }


        /// <summary>
        /// There are no comments for pe_grmu_id in the schema.
        /// </summary>
        public virtual string pe_grmu_id {
            get {
                return this._pe_grmu_id;
            }
            set {
                if (this._pe_grmu_id != value) {
                    this.SendPropertyChanging();
                    this._pe_grmu_id = value;
                    this.SendPropertyChanged("pe_grmu_id");
                }
            }
        }
        
        /// <summary>
        /// There are no comments for u_version in the schema.
        /// </summary>
        public virtual string u_version {
            get {
                return this._u_version;
            }
            set {
                if (this._u_version != value) {
                    this.SendPropertyChanging();
                    this._u_version = value;
                    this.SendPropertyChanged("u_version");
                }
            }
        }


        /// <summary>
        /// There are no comments for libf_grmu in the schema.
        /// </summary>
        public virtual string libf_grmu {
            get {
                return this._libf_grmu;
            }
            set {
                if (this._libf_grmu != value) {
                    this.SendPropertyChanging();
                    this._libf_grmu = value;
                    this.SendPropertyChanged("libf_grmu");
                }
            }
        }


        /// <summary>
        /// There are no comments for libd_grmu in the schema.
        /// </summary>
        public virtual string libd_grmu {
            get {
                return this._libd_grmu;
            }
            set {
                if (this._libd_grmu != value) {
                    this.SendPropertyChanging();
                    this._libd_grmu = value;
                    this.SendPropertyChanged("libd_grmu");
                }
            }
        }


        /// <summary>
        /// There are no comments for libe_grmu in the schema.
        /// </summary>
        public virtual string libe_grmu {
            get {
                return this._libe_grmu;
            }
            set {
                if (this._libe_grmu != value) {
                    this.SendPropertyChanging();
                    this._libe_grmu = value;
                    this.SendPropertyChanged("libe_grmu");
                }
            }
        }


        /// <summary>
        /// There are no comments for libi_grmu in the schema.
        /// </summary>
        public virtual string libi_grmu {
            get {
                return this._libi_grmu;
            }
            set {
                if (this._libi_grmu != value) {
                    this.SendPropertyChanging();
                    this._libi_grmu = value;
                    this.SendPropertyChanged("libi_grmu");
                }
            }
        }


        /// <summary>
        /// There are no comments for description in the schema.
        /// </summary>
        public virtual string description {
            get {
                return this._description;
            }
            set {
                if (this._description != value) {
                    this.SendPropertyChanging();
                    this._description = value;
                    this.SendPropertyChanged("description");
                }
            }
        }


        /// <summary>
        /// There are no comments for no_ip in the schema.
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


        /// <summary>
        /// There are no comments for type_grmu in the schema.
        /// </summary>
        public virtual string type_grmu {
            get {
                return this._type_grmu;
            }
            set {
                if (this._type_grmu != value) {
                    this.SendPropertyChanging();
                    this._type_grmu = value;
                    this.SendPropertyChanged("type_grmu");
                }
            }
        }


        /// <summary>
        /// There are no comments for user_cre in the schema.
        /// </summary>
        public virtual string user_cre {
            get {
                return this._user_cre;
            }
            set {
                if (this._user_cre != value) {
                    this.SendPropertyChanging();
                    this._user_cre = value;
                    this.SendPropertyChanged("user_cre");
                }
            }
        }


        /// <summary>
        /// There are no comments for dh_cre in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> dh_cre {
            get {
                return this._dh_cre;
            }
            set {
                if (this._dh_cre != value) {
                    this.SendPropertyChanging();
                    this._dh_cre = value;
                    this.SendPropertyChanged("dh_cre");
                }
            }
        }


        /// <summary>
        /// There are no comments for user_maj in the schema.
        /// </summary>
        public virtual string user_maj {
            get {
                return this._user_maj;
            }
            set {
                if (this._user_maj != value) {
                    this.SendPropertyChanging();
                    this._user_maj = value;
                    this.SendPropertyChanged("user_maj");
                }
            }
        }


        /// <summary>
        /// There are no comments for dh_maj in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> dh_maj {
            get {
                return this._dh_maj;
            }
            set {
                if (this._dh_maj != value) {
                    this.SendPropertyChanging();
                    this._dh_maj = value;
                    this.SendPropertyChanged("dh_maj");
                }
            }
        }


        /// <summary>
        /// There are no comments for pe_cfgd_list in the schema.
        /// </summary>
        public virtual IList<pe_cfgd> pe_cfgd_list {
            get {
                return this._pe_cfgd_list;
            }
            set {
                this._pe_cfgd_list = value;
                this.SendPropertyChanged("pe_cfgd_list");
            }
        }


        /// <summary>
        /// There are no comments for pe_gmmu_list in the schema.
        /// </summary>
        public virtual IList<pe_gmmu> pe_gmmu_list {
            get {
                return this._pe_gmmu_list;
            }
            set {
                this._pe_gmmu_list = value;
                this.SendPropertyChanged("pe_gmmu_list");
            }
        }
        
        /// <summary>
        /// There are no comments for pe_gmes_list in the schema.
        /// </summary>
        public virtual IList<pe_gmes> pe_gmes_list {
            get {
                return this._pe_gmes_list;
            }
            set {
                this._pe_gmes_list = value;
                this.SendPropertyChanged("pe_gmes_list");
            }
        }

        public virtual pe_ip pe_ip {
            get {
                return this._pe_ip;
            }
            set {
                if (this._pe_ip != value) {
                    this.SendPropertyChanging();
                    this._pe_ip = value;
                    this.SendPropertyChanged("pe_ip");
                }
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

        public virtual IList<pe_muta> pe_muta_list {
            get {
                string[] ar_muta_id = (from g in pe_gmmu_list select g.pe_muta_id).ToArray();

                _pe_muta_list = hibernate_util.get_instance().get_current_session().CreateCriteria<pe_muta>().Add(Restrictions.In("pe_muta_id",ar_muta_id)).List<pe_muta>();

                // _pe_muta_list = hibernate_util.get_instance().get_current_session().CreateCriteria<pe_muta>().List<pe_muta>().Where(x => x.no_ip == no_ip &
                //  x.type_grmu.Equals(type_grmu)                                                                                                                  

                return _pe_muta_list;
            }
            set {
                this.SendPropertyChanged("pe_muta_list");
            }
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                pe_grmu grmu = (pe_grmu)value;
                return grmu.pe_muta_list;
            } catch {
            }
            return null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        public virtual bool is_expanded => false;
    }
}
