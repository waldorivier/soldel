using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using mupeModel;
using mupeModel.Utils;
using System.Collections;

namespace Soldel.Views {
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class w_meal : Window {

        // to integrate in uc_connection
        internal persistant_controller persistant_controller { get; set; }

        // specifies the class name to manage
        private String _class_name = null;

        public w_meal(String class_name) {
            this._class_name = class_name;
            InitializeComponent();
        }
        
        private void dg_meal_content_add(object sender, DataGridCellEditEndingEventArgs e) {
            if (element.DataContext is meal) {
                meal meal = (meal)element.DataContext;

                meal_content mc = (meal_content)e.EditingElement.DataContext;
                // n'insérer un nouvel aliment ssi le contexte est vide; sinon il s'agit d'une modification
                if (mc.meal_id == 0) {
                    ComboBox cb = (ComboBox)e.EditingElement;
                    food food = (food)cb.SelectedItem;
                    if (food != null) {
                        mc = new meal_content(meal, food);
                        if (meal.can_add_child(mc)) {
                            meal.add_child(mc);
                        }
                    }
                }
            }
        }
        
        #region COMMAND HANDLER

        private void load_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = uc_select_connection.session != null;
        }

        private void load_executed(object sender, ExecutedRoutedEventArgs e) {
            ISession session = uc_select_connection.session;

            IList l = null;
            if (this._class_name == "meal_content") {
                 l = session.CreateCriteria<meal>().List<meal>().ToList();
             } else {
                l = session.CreateCriteria<food>().List<food>().ToList();
            }

            l_element.ItemsSource = l;
            l_element.SelectedItem = null;
            persistant_controller = new persistant_controller(session);
        }

        private void validate_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = l_element.SelectedItem != null;
        }

        private void validate_executed(object sender, ExecutedRoutedEventArgs e) {
            i_soldel elem = (i_soldel)element.DataContext;

            // TODO : déplacer dans persistant_controller ?
            if (elem != null) {
                if (elem.can_update()) {
                    elem.update();
                }
                persistant_controller.update(elem);
            }

            l_element.SelectedItem = null;
            element.DataContext = null;
        }

        private void copy_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = l_element.SelectedItem != null;
        }

        private void copy_executed(object sender, ExecutedRoutedEventArgs e) {
            i_soldel to_copy = (i_soldel)l_element.SelectedItem;

            i_soldel copy = to_copy.copy();
            element.DataContext = copy;
            persistant_controller.update(copy);

            l_element.SelectedItem = null;
            element.DataContext = null;
        }

        private void update_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = l_element.SelectedItem != null;
        }

        private void update_executed(object sender, ExecutedRoutedEventArgs e) {
            element.DataContext = l_element.SelectedItem;
        }

        private void delete_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = l_element.SelectedItem != null;
        }

        private void delete_executed(object sender, ExecutedRoutedEventArgs e) {
            persistant_controller.delete(null, (i_soldel)l_element.SelectedItem);
            l_element.SelectedItem = null;
            element.DataContext = null;
        }

        private void cancel_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = l_element.SelectedItem != null;
        }

        private void cancel_executed(object sender, ExecutedRoutedEventArgs e) {
            element = null;
            l_element.SelectedItem = null;
            element.DataContext = null;
        }

        private void add_can_execute(object sender, CanExecuteRoutedEventArgs e) {
        }

        private void add_executed(object sender, ExecutedRoutedEventArgs e) {
        }

        #endregion
    }
}
