namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class attr_command {

        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(attr_command), null);
        private static RoutedUICommand _paste = new RoutedUICommand("paste","paste",typeof(attr_command),null);
        private static RoutedUICommand _add = new RoutedUICommand("add","add",typeof(attr_command),null);
        private static RoutedUICommand _delete = new RoutedUICommand("delete","delete",typeof(attr_command),null);

        public static RoutedUICommand copy {
            get => _copy;
            set => _copy = value;
        }
        
        public static RoutedUICommand paste {
            get => _paste;
            set => _paste = value;
        }

        public static RoutedUICommand add {
            get => _add;
            set => _add = value;
        }
        public static RoutedUICommand delete {
            get => _delete;
            set => _delete = value;
        }
    }
}
