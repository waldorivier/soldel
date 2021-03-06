﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace mupeModel {
    class global_dict : INotifyPropertyChanged, i_soldel {

        public virtual event PropertyChangedEventHandler PropertyChanged;

        private string _dict_list_name; 
        private IList<pe_dict> _dict_list;
        private IList<pe_libl> _libl_list;

        public global_dict() {
            _dict_list = new ObservableCollection<pe_dict>();
            _libl_list = new ObservableCollection<pe_libl>();
        }

        public IList<pe_dict> dict_list {
            get => _dict_list;

            set {
                _dict_list = value;
                SendPropertyChanged("dict_list");
            }
        }
        
        public IList<pe_libl> libl_list {
            get => _libl_list;

            set {
                _libl_list = value;
                SendPropertyChanged("libl_list");
            }
        }

        public string dict_list_name {
            get => _dict_list_name;
            set => _dict_list_name = value;
        }

        public bool is_expanded => true;

        protected virtual void SendPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if(propertyChanged != null) {
                propertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
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
            return false;
        }
    }
}
