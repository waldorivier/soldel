using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace mupeModel {
    class global : IValueConverter {

        private string _dict_list_name; 

        private IList<pe_dict> _dict_list;
        private IList<pe_libl> _libl_list;

        public global() {
            _dict_list = new List<pe_dict>();
            _libl_list = new List<pe_libl>();
        }

        public IList<pe_dict> dict_list{ get => _dict_list; set => _dict_list = value; }

        public IList<pe_libl> libl_list {
            get => _libl_list; set => _libl_list = value; }
        public string dict_list_name { get => _dict_list_name; set => _dict_list_name = value; }

        // TODO : Convert un seul des liste

        object IValueConverter.Convert(object value,Type targetType,object parameter,CultureInfo culture) {
            return _dict_list;
        }

        object IValueConverter.ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
            return null;
        }
    }
}
