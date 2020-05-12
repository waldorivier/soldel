namespace mupeModel.Commands {
    using System;
    using System.Windows.Input;

    public static class meal_content_command {

        public static RoutedUICommand add { get; set; } = new RoutedUICommand("add", "add", typeof(meal_content_command), null);
    }
}

