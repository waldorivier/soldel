namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class muta_command {
        private static RoutedUICommand _copy = new RoutedUICommand("copy", "copy", typeof(muta_command), null);

        public static RoutedUICommand copy {
            get => _copy;
            set => _copy = value;
        }
    }
}