﻿using System;
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
using NHibernate.Collection.Observable;
using mupeModel;

namespace Soldel.Views {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class w_main : Window {
        public w_main() {
            try {
                InitializeComponent();

                btn_meal.Click += Btn_meal_Click;
                btn_food.Click += Btn_food_Click;

            } catch (Exception ex) {
                MessageBox.Show(ex.StackTrace);            }
        }

        private void Btn_food_Click(object sender, RoutedEventArgs e) {
            new w_meal("food").ShowDialog();
        }

        private void Btn_meal_Click(object sender, RoutedEventArgs e) {
            new w_meal("meal_content").ShowDialog();
        }
    }
}
