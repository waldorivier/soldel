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
using mupeModel.Utils;

namespace mupeModel {

    /// <summary>
    /// There are no comments for pe_libl, Soldel in the schema.
    /// </summary>
    public partial class pe_libl: soldel, INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private int _no_ip;

        private string _nom_attr;

        private string _u_version;

        private string _libf_attr;

        private string _libd_attr;

        private string _libe_attr;

        private string _libi_attr;

        private string _lif_valeur;

        private string _lid_valeur;

        private string _lie_valeur;

        private string _lii_valeur;

        private string _groupe;

        private string _hintf;

        private string _hintd;

        private string _hinte;

        private string _hinti;

        private pe_ip _pe_ip;

        private string _user_cre;

        private System.Nullable<System.DateTime> _dh_cre;

        private string _user_maj;

        private System.Nullable<System.DateTime> _dh_maj;

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        public override bool Equals(object obj) {
            pe_libl toCompare = obj as pe_libl;
            if(toCompare == null) {
                return false;
            }

            if(!Object.Equals(this.no_ip,toCompare.no_ip))
                return false;
            if(!Object.Equals(this.nom_attr,toCompare.nom_attr))
                return false;

            return true;
        }

        public override int GetHashCode() {
            int hashCode = 13;
            hashCode = (hashCode * 7) + no_ip.GetHashCode();
            hashCode = (hashCode * 7) + nom_attr.GetHashCode();
            return hashCode;
        }

        #endregion
        /// <summary>
        /// There are no comments for pe_libl constructor in the schema.
        /// </summary>
        public pe_libl() {
            this._no_ip = 0;
            this._nom_attr = @"";
            OnCreated();
        }


        /// <summary>
        /// There are no comments for no_ip in the schema.
        /// </summary>
        public virtual int no_ip {
            get {
                return this._no_ip;
            }
            set {
                if(this._no_ip != value) {
                    this.SendPropertyChanging();
                    this._no_ip = value;
                    this.SendPropertyChanged("no_ip");
                }
            }
        }


        /// <summary>
        /// There are no comments for nom_attr in the schema.
        /// </summary>
        public virtual string nom_attr {
            get {
                return this._nom_attr;
            }
            set {
                if(this._nom_attr != value) {
                    this.SendPropertyChanging();
                    this._nom_attr = value;
                    this.SendPropertyChanged("nom_attr");
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
        /// There are no comments for libf_attr in the schema.
        /// </summary>
        public virtual string libf_attr {
            get {
                return this._libf_attr;
            }
            set {
                if(this._libf_attr != value) {
                    this.SendPropertyChanging();
                    this._libf_attr = value;
                    this.SendPropertyChanged("libf_attr");
                }
            }
        }


        /// <summary>
        /// There are no comments for libd_attr in the schema.
        /// </summary>
        public virtual string libd_attr {
            get {
                return this._libd_attr;
            }
            set {
                if(this._libd_attr != value) {
                    this.SendPropertyChanging();
                    this._libd_attr = value;
                    this.SendPropertyChanged("libd_attr");
                }
            }
        }


        /// <summary>
        /// There are no comments for libe_attr in the schema.
        /// </summary>
        public virtual string libe_attr {
            get {
                return this._libe_attr;
            }
            set {
                if(this._libe_attr != value) {
                    this.SendPropertyChanging();
                    this._libe_attr = value;
                    this.SendPropertyChanged("libe_attr");
                }
            }
        }


        /// <summary>
        /// There are no comments for libi_attr in the schema.
        /// </summary>
        public virtual string libi_attr {
            get {
                return this._libi_attr;
            }
            set {
                if(this._libi_attr != value) {
                    this.SendPropertyChanging();
                    this._libi_attr = value;
                    this.SendPropertyChanged("libi_attr");
                }
            }
        }


        /// <summary>
        /// There are no comments for lif_valeur in the schema.
        /// </summary>
        public virtual string lif_valeur {
            get {
                return this._lif_valeur;
            }
            set {
                if(this._lif_valeur != value) {
                    this.SendPropertyChanging();
                    this._lif_valeur = value;
                    this.SendPropertyChanged("lif_valeur");
                }
            }
        }


        /// <summary>
        /// There are no comments for lid_valeur in the schema.
        /// </summary>
        public virtual string lid_valeur {
            get {
                return this._lid_valeur;
            }
            set {
                if(this._lid_valeur != value) {
                    this.SendPropertyChanging();
                    this._lid_valeur = value;
                    this.SendPropertyChanged("lid_valeur");
                }
            }
        }


        /// <summary>
        /// There are no comments for lie_valeur in the schema.
        /// </summary>
        public virtual string lie_valeur {
            get {
                return this._lie_valeur;
            }
            set {
                if(this._lie_valeur != value) {
                    this.SendPropertyChanging();
                    this._lie_valeur = value;
                    this.SendPropertyChanged("lie_valeur");
                }
            }
        }


        /// <summary>
        /// There are no comments for lii_valeur in the schema.
        /// </summary>
        public virtual string lii_valeur {
            get {
                return this._lii_valeur;
            }
            set {
                if(this._lii_valeur != value) {
                    this.SendPropertyChanging();
                    this._lii_valeur = value;
                    this.SendPropertyChanged("lii_valeur");
                }
            }
        }


        /// <summary>
        /// There are no comments for groupe in the schema.
        /// </summary>
        public virtual string groupe {
            get {
                return this._groupe;
            }
            set {
                if(this._groupe != value) {
                    this.SendPropertyChanging();
                    this._groupe = value;
                    this.SendPropertyChanged("groupe");
                }
            }
        }


        /// <summary>
        /// There are no comments for hintf in the schema.
        /// </summary>
        public virtual string hintf {
            get {
                return this._hintf;
            }
            set {
                if(this._hintf != value) {
                    this.SendPropertyChanging();
                    this._hintf = value;
                    this.SendPropertyChanged("hintf");
                }
            }
        }


        /// <summary>
        /// There are no comments for hintd in the schema.
        /// </summary>
        public virtual string hintd {
            get {
                return this._hintd;
            }
            set {
                if(this._hintd != value) {
                    this.SendPropertyChanging();
                    this._hintd = value;
                    this.SendPropertyChanged("hintd");
                }
            }
        }


        /// <summary>
        /// There are no comments for hinte in the schema.
        /// </summary>
        public virtual string hinte {
            get {
                return this._hinte;
            }
            set {
                if(this._hinte != value) {
                    this.SendPropertyChanging();
                    this._hinte = value;
                    this.SendPropertyChanged("hinte");
                }
            }
        }


        /// <summary>
        /// There are no comments for hinti in the schema.
        /// </summary>
        public virtual string hinti {
            get {
                return this._hinti;
            }
            set {
                if(this._hinti != value) {
                    this.SendPropertyChanging();
                    this._hinti = value;
                    this.SendPropertyChanged("hinti");
                }
            }
        }

        public virtual pe_ip pe_ip {
            get {
                return this._pe_ip;
            }
            set {
                if(this._pe_ip != value) {
                    this.SendPropertyChanging();
                    this._pe_ip = value;
                    this.SendPropertyChanged("pe_ip");
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

        public virtual pe_libl shallow_copy() {
            var copy = new pe_libl();
            copy_object.copy<pe_libl>(this,copy);

            copy.no_ip = 0;

            return copy;
        }
    }
}
