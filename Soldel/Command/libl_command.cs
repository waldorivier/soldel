namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class libl_command {
        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(libl_command), null);
        private static RoutedUICommand _paste = new RoutedUICommand("paste", "paste", typeof(libl_command), null);
        private static RoutedUICommand _add = new RoutedUICommand("add","add",typeof(libl_command),null);

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
    }
}
