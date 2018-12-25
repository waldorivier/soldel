namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class chat_box_command {

        private static RoutedUICommand _accept = new RoutedUICommand("accept", "accept", typeof(chat_box_command), null);
        private static RoutedUICommand _reject = new RoutedUICommand("reject", "reject", typeof(chat_box_command), null);
        private static RoutedUICommand _cancel = new RoutedUICommand("cancel", "cancel", typeof(chat_box_command), null);

        public static RoutedUICommand accept {
            get => _accept;
            set => _accept = value;
        }

        public static RoutedUICommand reject {
            get => _reject;
            set => _reject = value;
        }

        public static RoutedUICommand cancel {
            get => _cancel;
            set => _cancel = value;
        }
    }
}
