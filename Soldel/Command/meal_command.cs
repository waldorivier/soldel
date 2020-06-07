namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class meal_command {

        private static RoutedUICommand _load = new RoutedUICommand("load", "load", typeof(meal_command), null);
        private static RoutedUICommand _update = new RoutedUICommand("update", "update", typeof(meal_command), null);
        private static RoutedUICommand _create = new RoutedUICommand("create", "create", typeof(meal_command), null);
        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(meal_command), null);
        private static RoutedUICommand _delete = new RoutedUICommand("delete", "delete", typeof(meal_command), null);
        private static RoutedUICommand _cancel = new RoutedUICommand("cancel", "cancel", typeof(meal_command), null);

        public static RoutedUICommand load {
            get => _load;
            set => _load = value;
        }

        public static RoutedUICommand create{
            get => _create;
            set => _create = value;
        }

        public static RoutedUICommand update {
            get => _update;
            set => _update = value;
        }

        public static RoutedUICommand copy {
            get => _copy;
            set => _copy = value;
        }

        public static RoutedUICommand delete {
            get => _delete;
            set => _delete = value;
        }

        public static RoutedUICommand cancel {
            get => _cancel;
            set => _cancel = value;
        }
    }


}
