namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class dict_command {
        private static RoutedUICommand _select = new RoutedUICommand("select", "select", typeof(dict_command), null);
        private static RoutedUICommand _add = new RoutedUICommand("add","add",typeof(dict_command),null);

        public static RoutedUICommand select {
            get => _select;
            set => _select = value;
        }

        public static RoutedUICommand add {
            get => _add;
            set => _add = value;
        }
    }
}
