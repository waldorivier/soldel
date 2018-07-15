using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mupeModel.Command {
    class test_command {

        private static RoutedUICommand test;

        static test_command() {
            Test = new RoutedUICommand("test", "test", typeof(test_command), null);
        }

        public static RoutedUICommand Test { get => test; set => test = value; }
    }
}
