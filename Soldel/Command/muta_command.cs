namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class muta_command {

        private static RoutedUICommand copy_ = new RoutedUICommand("copy", "copy", typeof(muta_command), null);
        private static RoutedUICommand refresh_ = new RoutedUICommand("refresh","refresh",typeof(muta_command),null);

        public static RoutedUICommand copy {
            get => copy_;
            set => copy_ = value;
        }

        public static RoutedUICommand refresh {
            get => refresh_;
            set => refresh_ = value;
        }

    }
}
