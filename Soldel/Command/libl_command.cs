namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class libl_command {
        private static RoutedUICommand copy_ = new RoutedUICommand("copy", "copy", typeof(libl_command), null);

        public static RoutedUICommand copy {
            get => copy_;
            set => copy_ = value;
        }
    }
}
