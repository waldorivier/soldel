namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class dict_command {
        private static RoutedUICommand select_ = new RoutedUICommand("select", "select", typeof(dict_command), null);
        private static RoutedUICommand add_ = new RoutedUICommand("add","add",typeof(dict_command),null);

        public static RoutedUICommand select {
            get => select_;
            set => select_ = value;
        }

        public static RoutedUICommand add {
            get => add_;
            set => add_ = value;
        }
    }
}
