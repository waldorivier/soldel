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
    /// There are no comments for pe_cfgd, Soldel in the schema.
    /// </summary>
    public partial class pe_cfgd : INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private string _pe_cfgt_id;

        private string _pe_grmu_id;

        private string _u_version;

        private string _cfg_xml;

        private string _user_cre;

        private System.Nullable<System.DateTime> _dh_cre;

        private string _user_maj;

        private System.Nullable<System.DateTime> _dh_maj;

        private pe_cfgt _pe_cfgt;

        private pe_grmu _pe_grmu;

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        public override bool Equals(object obj) {
            pe_cfgd toCompare = obj as pe_cfgd;
            if (toCompare == null) {
                return false;
            }

            if (!Object.Equals(this.pe_cfgt_id, toCompare.pe_cfgt_id))
                return false;
            if (!Object.Equals(this.pe_grmu_id, toCompare.pe_grmu_id))
                return false;

            return true;
        }

        public override int GetHashCode() {
            int hashCode = 13;
            hashCode = (hashCode * 7) + pe_cfgt_id.GetHashCode();
            hashCode = (hashCode * 7) + pe_grmu_id.GetHashCode();
            return hashCode;
        }

        #endregion
        /// <summary>
        /// There are no comments for pe_cfgd constructor in the schema.
        /// </summary>
        public pe_cfgd() {
            this._pe_cfgt_id = @"";
            this._pe_grmu_id = @"";
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
        /// There are no comments for cfg_xml in the schema.
        /// </summary>
        public virtual string cfg_xml {
            get {
                return this._cfg_xml;
            }
            set {
                if (this._cfg_xml != value) {
                    this.SendPropertyChanging();
                    this._cfg_xml = value;
                    this.SendPropertyChanged("cfg_xml");
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
        /// There are no comments for pe_cfgt in the schema.
        /// </summary>
        public virtual pe_cfgt pe_cfgt {
            get {
                return this._pe_cfgt;
            }
            set {
                if (this._pe_cfgt != value) {
                    this.SendPropertyChanging();
                    this._pe_cfgt = value;
                    this.SendPropertyChanged("pe_cfgt");
                }
            }
        }


        /// <summary>
        /// There are no comments for pe_grmu in the schema.
        /// </summary>
        public virtual pe_grmu pe_grmu {
            get {
                return this._pe_grmu;
            }
            set {
                if (this._pe_grmu != value) {
                    this.SendPropertyChanging();
                    this._pe_grmu = value;
                    this.SendPropertyChanged("pe_grmu");
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
