namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class attr_command {
        private static RoutedUICommand copy_ = new RoutedUICommand("copy", "copy", typeof(attr_command), null);
        private static RoutedUICommand add_ = new RoutedUICommand("add","add",typeof(attr_command),null);

        public static RoutedUICommand copy {
            get => copy_;
            set => copy_ = value;
        }

        public static RoutedUICommand add {
            get => add_;
            set => add_ = value;
        }
    }
}
