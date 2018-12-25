namespace mupeModel.Views.Converters {
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    internal class folder_node:IValueConverter {
        object IValueConverter.Convert(object value,Type targetType,object parameter,CultureInfo culture) =>
            // this.child_nodes;
            null;

        object IValueConverter.ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
            throw new NotImplementedException();
        }

        public string name { get; internal set; }

        public IEnumerable child_nodes { get; internal set; }
    }
}
