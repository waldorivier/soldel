using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mupeModel.Commands {
    public class generate_config_command : ICommand {
        event EventHandler ICommand.CanExecuteChanged {
            add {
                // throw new NotImplementedException();
            }

            remove {
                // throw new NotImplementedException();
            }
        }

        bool ICommand.CanExecute(object parameter) {
            return true;
        }

        void ICommand.Execute(object parameter) {
            Process.Start("N:\\04 IT\\BusinessTechnology\\Projects\\SelfService\\Migration\\1 outils Migration config\\MUPE2XML.lnk");
        }
    }
}
