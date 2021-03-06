namespace mupeModel.Views.Converters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    internal class w_generic_tree_multi_list_converter:IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            string str = (parameter as string) ?? "";
            char[] separator = new char[] { ',' };
            List<string> list = (from f in str.Split(separator) select f.Trim()).ToList<string>();
            while(values.Length > list.Count) {
                list.Add(string.Empty);
            }
            List<object> list2 = new List<object>();
            for(int i = 0;i < values.Length;i++) {
                IEnumerable enumerable;
                if(values[i] is IEnumerable) {
                    enumerable = values[i] as IEnumerable;
                } else {
                    List<object> list1 = new List<object> {
                        values[i]
                    };
                    enumerable = list1;
                }
                string str2 = list[i];
                if(str2 != string.Empty) {
                    folder_node item = new folder_node {
                        name = str2,
                        child_nodes = enumerable
                    };
                    list2.Add(item);
                } else {
                    foreach(object obj2 in enumerable) {
                        list2.Add(obj2);
                    }
                }
            }
            return list2;
        }

        public object[] ConvertBack(object value,Type[] targetTypes,object parameter,CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
