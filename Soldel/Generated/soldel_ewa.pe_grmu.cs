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
using System.Windows.Input;
using mupeModel.Views;

namespace mupeModel {

    /// <summary>
    /// There are no comments for pe_grmu, Soldel in the schema.
    /// </summary>
    public partial class pe_grmu:soldel, INotifyPropertyChanging, INotifyPropertyChanged {

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
                if(this._pe_grmu_id != value) {
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
                if(this._u_version != value) {
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
                if(this._libf_grmu != value) {
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
                if(this._libd_grmu != value) {
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
                if(this._libe_grmu != value) {
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
                if(this._libi_grmu != value) {
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
                if(this._description != value) {
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
                if(this._no_ip != value) {
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
                if(this._type_grmu != value) {
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
                if(this._pe_ip != value) {
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

        protected virtual IList<pe_muta> initialize_muta_list() {
            if (pe_gmmu_list.Count > 0) {
                var mutas = (from x in this.pe_gmmu_list select x.pe_muta).ToList();
                return mutas;
            }
            return null;
        }

        public virtual IList<pe_muta> pe_muta_list {
            get {
                return initialize_muta_list();
            }

            set {
                // _pe_muta_list = initialize_muta_list();
                this.SendPropertyChanged("pe_muta_list");
            }
        }
         
        public virtual IList<String> type_grmu_list {
            get {
                return new List<String>() { "01", "03" };
            }
        }

        public virtual IList<pe_muta> datagrid_list {
            get => pe_muta_list;
        }

        // TODO : proto devant encore �tre g�n�ralis� 
        private class muta_action {
            private pe_muta _muta = null;
            private pe_grmu _grmu = null;
            private persistant_controller _persistant_controller = null;

            public Action action_1;
            public Action action_2;

            public pe_muta muta { get => _muta; set => _muta = value; }

            public muta_action(pe_grmu grmu, pe_muta muta, persistant_controller persistant_controller) {
                _muta = muta;
                _grmu = grmu;
                _persistant_controller = persistant_controller;

                action_1 = paste_muta_deep;
                action_2 = paste_muta_reference;
            }

            private void paste_muta_deep() {
                _muta = _muta.deep_copy(hibernate_util.get_instance().generate_muta_id(), _grmu.pe_ip);
                _persistant_controller.update(_muta);
            }

            private void paste_muta_reference() {
            }
        }

        public virtual void paste_muta(pe_muta muta) {
            var persistant_controller = new persistant_controller(hibernate_util.get_instance().get_current_session());
            muta_action muta_action = new muta_action(this, muta, persistant_controller);

            // copie d'une mutation pour la m�me ip => seule la r�f�rence est ajout�e (pas de copie effectu�e)
            // si l'on ne veut pas ajouter une r�f�rence, il faut copier une mutation despuis une autre ip
            var box = new chatbot_box (
                "Faire une copie ou ajouter simplement une r�f�rence (ceci n'�tant possible " +
                "que si l'IP source et identique � l'IP destination)",
                muta_action.action_1,
                muta_action.action_2
            ).ShowDialog();

            pe_gmmu gmmu = new pe_gmmu(this, muta_action.muta ?? muta);
            persistant_controller.update(gmmu);
            persistant_controller.session.Refresh(this);

            // provoque une mise � jour de la liste ! 
            pe_muta_list = null;
        }

        #region I_SOLDEL

        public virtual pe_grmu shallow_copy(string grmu_id, mupeModel.pe_ip ip) {
            pe_grmu copy = new pe_grmu();
            copy_object.copy<pe_grmu>(this, copy);

            copy.pe_grmu_id = grmu_id;
            copy.no_ip = ip.no_ip;
            copy.pe_ip = ip;

            return copy;
        }

        #endregion
        
    }
}
