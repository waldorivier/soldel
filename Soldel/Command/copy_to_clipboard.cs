using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mupeModel.Command {
    class copy_to_clipboard : ICommand {
        event EventHandler ICommand.CanExecuteChanged {
            add {
                // throw new NotImplementedException();
            }

            remove {
                // throw new NotImplementedException();
            }
        }

        bool ICommand.CanExecute(object parameter) {
            var s = Clipboard.GetData("String");
            return s == null;
        }

        void ICommand.Execute(object parameter) {
            var muta_id = parameter as string; 
            if (muta_id != null)
                Clipboard.SetData("String", parameter);
        }
    }
}
