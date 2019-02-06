using System;
using System.Windows;
using System.Windows.Input;

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

            this.btn_accept.Content = "Copier";
            this.btn_reject.Content = "Ajouter une référence";
        }

        private void init_dialog(string question) {
            this.question.Text = question;
        }

        #region COMMAND HANDLER

        private void accept_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (response.Text != "");
            e.CanExecute = true;
        }

        private void accept_executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
            _option_1();
        }

        private void reject_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (response.Text != "");
            e.CanExecute = true;    
        }

        private void reject_executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
        }

        private void cancel_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            // e.CanExecute = (response.Text != "");
        }

        private void cancel_executed(object sender, ExecutedRoutedEventArgs e) {
        }

        #endregion
    }
}
