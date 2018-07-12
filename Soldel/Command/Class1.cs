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
            return Clipboard.GetText() is null;
        }

        void ICommand.Execute(object parameter) {
            
        }
    }
}
