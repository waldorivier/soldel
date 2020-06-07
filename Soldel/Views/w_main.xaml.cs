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
using mupeModel;
using NHibernate;

namespace Soldel.Views {
      public partial class w_main : Window {
        private ISession _session = null;

        public w_main() {
            try {
                InitializeComponent();

                btn_meal.Click += Btn_meal_Click;
                btn_food.Click += Btn_food_Click;
                btn_symptom.Click += Btn_symptom_Click;

               
            } catch (Exception ex) {
                MessageBox.Show(ex.StackTrace);            }
        }

        private void Btn_meal_Click(object sender, RoutedEventArgs e) {
            _session = uc_select_connection.session;
            w_meal w = new w_meal("meal", _session );
            w.Height = 700;
            w.Width = 700;

            w.ShowDialog();
        }

        private void Btn_food_Click(object sender, RoutedEventArgs e) {
            _session = uc_select_connection.session;
            w_meal w = new w_meal("food", _session);
            w.Height = 450;
            w.Width = 500;

            w.ShowDialog();
        }

        private void  Btn_symptom_Click (object sender, RoutedEventArgs e) {
            _session = uc_select_connection.session;
            w_meal w = new w_meal("symptom", _session);
            w.Height = 450;
            w.Width = 500;

            w.ShowDialog();
        }
    }
}
