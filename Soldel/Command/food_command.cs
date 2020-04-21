namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    class food_command {
        private static RoutedUICommand _validate = new RoutedUICommand("validate", "validate", typeof(food_command), null);
        private static RoutedUICommand _load = new RoutedUICommand("load", "load", typeof(food_command), null);

        public static RoutedUICommand validate {
            get => _validate;
            set => _validate = value;
        }

        public static RoutedUICommand load {
            get => _load;
            set => _load = value;
        }
    }
}
