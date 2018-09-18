namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class muta_command {
        private static RoutedUICommand copy_ = new RoutedUICommand("copy", "copy", typeof(muta_command), null);

        public static RoutedUICommand copy {
            get => copy_;
            set => copy_ = value;
        }
    }
}
