namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class chat_box_command {

        private static RoutedUICommand validate_ = new RoutedUICommand("validate", "validate", typeof(attr_command), null);
        private static RoutedUICommand cancel_ = new RoutedUICommand("add","add",typeof(attr_command),null);

        public static RoutedUICommand validate {
            get => validate_;
            set => validate_ = value;
        }

        public static RoutedUICommand cancel {
            get => cancel_;
            set => cancel_ = value;
        }
    }
}
