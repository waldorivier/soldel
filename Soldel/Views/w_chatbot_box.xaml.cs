using mupeModel.Utils;
using Soldel.Views;
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

        private Action _option_1;
        private Action _option_2;

        public chatbot_box (string question, Action option_1, Action option_2 ) {
            InitializeComponent();
            init_dialog(question);

            _option_1 = option_1;
            _option_2 = option_2;

        }

        private void init_dialog(string question) {
            this.question.Text = question;
        }

        #region COMMAND HANDLER

        private void accept_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (response.Text != "");
        }

        private void accept_executed(object sender, ExecutedRoutedEventArgs e) {
            _option_1();
        }

        private void reject_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            // e.CanExecute = (response.Text != "");
        }

        private void reject_executed(object sender, ExecutedRoutedEventArgs e) {
        }

        private void cancel_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            // e.CanExecute = (response.Text != "");
        }

        private void cancel_executed(object sender, ExecutedRoutedEventArgs e) {
        }

        #endregion
    }
}
