using mupeModel;
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

namespace Soldel.Views {
    /// <summary>
    /// Logique d'interaction pour w_food_.xaml
    /// </summary>
    public partial class w_food : Window {

        internal persistant_controller persistant_controller { get; set; }

        public w_food() {
            InitializeComponent();
        }

        #region COMMAND HANDLER

        private void load_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = uc_select_connection.session != null;
        }

        private void load_executed(object sender, ExecutedRoutedEventArgs e) {
            List<food> l_food = uc_select_connection.session.CreateCriteria<food>().List<food>().ToList();

            dg_food_list.ItemsSource = l_food;
            persistant_controller = new persistant_controller(uc_select_connection.session);
        }

        private void validate_can_execute(object sender, CanExecuteRoutedEventArgs e) {
        }

        private void validate_executed(object sender, ExecutedRoutedEventArgs e) {
        }

        #endregion COMMAND HANDLER
    }
}
