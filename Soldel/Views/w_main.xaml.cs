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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class w_main : Window {
        public w_main() {
            try {
                InitializeComponent();

                btn_elca.Click += Btn_elca_Click;
                btn_elsi.Click += Btn_elsi_Click;
                btn_ewa.Click += Btn_ewa_Click;
            } catch (Exception ex) {
                MessageBox.Show(ex.StackTrace);            }
        }

        private void Btn_ewa_Click(object sender, RoutedEventArgs e) {
            new w_generic().ShowDialog();
        }

        private void Btn_elca_Click(object sender, RoutedEventArgs e) {
            new w_compare().ShowDialog();
        }

        public void Btn_elsi_Click(object sender, RoutedEventArgs e) {
            new w_elsi().ShowDialog();
        }
    }
}
