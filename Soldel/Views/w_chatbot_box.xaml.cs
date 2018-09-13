using mupeModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mupeModel.Views {
    /// <summary>
    /// </summary>
    public partial class chatbot_box : Window {
        public chatbot_box () {
            InitializeComponent();

            init_dialog();
        }

        private void init_dialog() {
            this.question.Text = "Renseigner un nom d''attribut que vous désirez ajouter au dictionnaire";
        }

        private void validate_can_execute(object sender,CanExecuteRoutedEventArgs e) {
            e.CanExecute = (response.Text != "");
        }

        #region COMMAND HANDLER

        private void validate_executed(object sender,ExecutedRoutedEventArgs e) {
            var session = hibernate_util.get_instance().get_current_session();
        }

        #endregion
    }
}
