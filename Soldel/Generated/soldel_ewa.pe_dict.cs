﻿//------------------------------------------------------------------------------
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
    /// There are no comments for pe_dict, Soldel in the schema.
    /// </summary>
    public partial class pe_dict:soldel, INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private string _pe_dict_id;

        private string _u_version;

        private string _nom_dict;

        private string _libf_dict;

        private string _libd_dict;

        private string _libe_dict;

        private string _libi_dict;

        private string _clatit_dict;

        private string _tydata_dict;

        private string _format_dict;

        private string _user_cre;

        private System.Nullable<System.DateTime> _dh_cre;

        private string _user_maj;

        private System.Nullable<System.DateTime> _dh_maj;

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for pe_dict constructor in the schema.
        /// </summary>
        public pe_dict() {
            this._pe_dict_id = @"";
            this._libf_dict = this.libd_dict = this._libe_dict = this._libi_dict = "NAN";
            this._clatit_dict = "base";
            this._nom_dict = @"NAN";

            OnCreated();
        }


        /// <summary>
        /// There are no comments for pe_dict_id in the schema.
        /// </summary>
        public virtual string pe_dict_id {
            get {
                return this._pe_dict_id;
            }
            set {
                if(this._pe_dict_id != value) {
                    this.SendPropertyChanging();
                    this._pe_dict_id = value;
                    this.SendPropertyChanged("pe_dict_id");
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
                if(this._u_version != value) {
                    this.SendPropertyChanging();
                    this._u_version = value;
                    this.SendPropertyChanged("u_version");
                }
            }
        }


        /// <summary>
        /// There are no comments for nom_dict in the schema.
        /// </summary>
        public virtual string nom_dict {
            get {
                return this._nom_dict;
            }
            set {
                if(this._nom_dict != value) {
                    this.SendPropertyChanging();
                    this._nom_dict = value;
                    this.SendPropertyChanged("nom_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for libf_dict in the schema.
        /// </summary>
        public virtual string libf_dict {
            get {
                return this._libf_dict;
            }
            set {
                if(this._libf_dict != value) {
                    this.SendPropertyChanging();
                    this._libf_dict = value;
                    this.SendPropertyChanged("libf_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for libd_dict in the schema.
        /// </summary>
        public virtual string libd_dict {
            get {
                return this._libd_dict;
            }
            set {
                if(this._libd_dict != value) {
                    this.SendPropertyChanging();
                    this._libd_dict = value;
                    this.SendPropertyChanged("libd_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for libe_dict in the schema.
        /// </summary>
        public virtual string libe_dict {
            get {
                return this._libe_dict;
            }
            set {
                if(this._libe_dict != value) {
                    this.SendPropertyChanging();
                    this._libe_dict = value;
                    this.SendPropertyChanged("libe_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for libi_dict in the schema.
        /// </summary>
        public virtual string libi_dict {
            get {
                return this._libi_dict;
            }
            set {
                if(this._libi_dict != value) {
                    this.SendPropertyChanging();
                    this._libi_dict = value;
                    this.SendPropertyChanged("libi_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for clatit_dict in the schema.
        /// </summary>
        public virtual string clatit_dict {
            get {
                return this._clatit_dict;
            }
            set {
                if(this._clatit_dict != value) {
                    this.SendPropertyChanging();
                    this._clatit_dict = value;
                    this.SendPropertyChanged("clatit_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for tydata_dict in the schema.
        /// </summary>
        public virtual string tydata_dict {
            get {
                return this._tydata_dict;
            }
            set {
                if(this._tydata_dict != value) {
                    this.SendPropertyChanging();
                    this._tydata_dict = value;
                    this.SendPropertyChanged("tydata_dict");
                }
            }
        }


        /// <summary>
        /// There are no comments for format_dict in the schema.
        /// </summary>
        public virtual string format_dict {
            get {
                return this._format_dict;
            }
            set {
                if(this._format_dict != value) {
                    this.SendPropertyChanging();
                    this._format_dict = value;
                    this.SendPropertyChanged("format_dict");
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
                if(this._user_cre != value) {
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
                if(this._dh_cre != value) {
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
                if(this._user_maj != value) {
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
                if(this._dh_maj != value) {
                    this.SendPropertyChanging();
                    this._dh_maj = value;
                    this.SendPropertyChanged("dh_maj");
                }
            }
        }

        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual event PropertyChangedEventHandler PropertyChanged;

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

        public virtual pe_libl convert_to_libl() {
            pe_libl libl = new pe_libl();

            libl.libd_attr = libd_dict;
            libl.libf_attr = libf_dict;
            libl.libi_attr = libi_dict;
            libl.libe_attr = libe_dict;

            return libl;
        }

        public virtual IList<String> classe_list {
            get {
                return new List<String>() { "base", "PE_CCLI", "PE_ELEV" };
            }
        }

        public virtual IList<String> tydata_list {
            get {
                return new List<String>() { "S", "N", "D"};
            }
        }

        #region DATAGRID

        public static IList<string> columns_to_display = new List<string>() { "user_maj", "dh_maj", "clatit_dict", "tydata_dict", "format_dict",
                                                                              "libf_dict", "libd_dict", "libe_dict", "libi_dict"};

        #endregion

    }
}
