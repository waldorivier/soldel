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

namespace mupeModel {

    /// <summary>
    /// There are no comments for pe_cfgt, Soldel in the schema.
    /// </summary>
    public partial class pe_cfgt : INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private string _pe_cfgt_id;

        private string _u_version;

        private int _no_ip;

        private System.DateTime _dadval;

        private string _description;

        private string _statut;

        private string _user_cre;

        private System.Nullable<System.DateTime> _dh_cre;

        private string _user_maj;

        private System.Nullable<System.DateTime> _dh_maj;

        private IList<pe_cfgd> _pe_cfgd_list;

        private pe_ip _pe_ip;

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for pe_cfgt constructor in the schema.
        /// </summary>
        public pe_cfgt() {
            this._pe_cfgt_id = @"";
            this._pe_cfgd_list = new List<pe_cfgd>();
            OnCreated();
        }


        /// <summary>
        /// There are no comments for pe_cfgt_id in the schema.
        /// </summary>
        public virtual string pe_cfgt_id {
            get {
                return this._pe_cfgt_id;
            }
            set {
                if (this._pe_cfgt_id != value) {
                    this.SendPropertyChanging();
                    this._pe_cfgt_id = value;
                    this.SendPropertyChanged("pe_cfgt_id");
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
        /// There are no comments for dadval in the schema.
        /// </summary>
        public virtual System.DateTime dadval {
            get {
                return this._dadval;
            }
            set {
                if (this._dadval != value) {
                    this.SendPropertyChanging();
                    this._dadval = value;
                    this.SendPropertyChanged("dadval");
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
        /// There are no comments for statut in the schema.
        /// </summary>
        public virtual string statut {
            get {
                return this._statut;
            }
            set {
                if (this._statut != value) {
                    this.SendPropertyChanging();
                    this._statut = value;
                    this.SendPropertyChanged("statut");
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
    }
}
