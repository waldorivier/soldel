
namespace mupeModel.Views.Converters {
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class w_generic_list_converter:IValueConverter {
        public object Convert(object value,Type targetType,object parameter,CultureInfo culture) {
            try {
                IValueConverter converter = (IValueConverter)value;
                return converter.Convert(value,targetType,parameter,culture);
            } catch {
            }
            return null;
        }

        public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
