namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class muta_command {

        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(muta_command), null);
        private static RoutedUICommand _refresh = new RoutedUICommand("refresh", "refresh",typeof(muta_command),null);
        private static RoutedUICommand _validate = new RoutedUICommand("validate", "validate",typeof(muta_command),null);

        public static RoutedUICommand copy {
            get => _copy;
            set => _copy = value;
        }

        public static RoutedUICommand refresh {
            get => _refresh;
            set => _refresh = value;
        }

        public static RoutedUICommand validate {
            get => _validate;
            set => _validate = value;
        }
    }
}
