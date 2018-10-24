namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class grmu_command {

        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(grmu_command), null);
        private static RoutedUICommand _add = new RoutedUICommand("add", "add", typeof(grmu_command),null);
        private static RoutedUICommand _delete = new RoutedUICommand("delete", "delete" ,typeof(grmu_command),null);
        private static RoutedUICommand _re_order_muta = new RoutedUICommand("re_order_muta", "re_order_muta", typeof(grmu_command),null);

        public static RoutedUICommand copy {
            get => _copy;
            set => _copy = value;
        }

        public static RoutedUICommand add {
            get => _add;
            set => _add = value;
        }
        public static RoutedUICommand delete {
            get => _delete;
            set => _delete = value;
        }
        public static RoutedUICommand re_order_muta {
            get => _re_order_muta;
            set => _re_order_muta = value;
        }
    }
}
