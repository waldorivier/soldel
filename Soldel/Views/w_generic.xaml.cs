﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Dynamic;
using System.Data;
using mupeModel.Utils;
using mupeModel;
using NHibernate;
using NHibernate.Criterion;
using mupeModel.Views;
using System.Collections.ObjectModel;

namespace Soldel.Views {

    /// <summary>
    /// Logique d'interaction pour w_generic.xaml
    /// </summary>
    public partial class w_generic:Window {

        private pe_attr _attr;
        private pe_libl _libl;
        private pe_muta _muta;

        private ISession _session;
        private bool test_dictionary = false;

        public w_generic() {

            InitializeComponent();

            Clipboard.Clear();

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = hibernate_util.get_instance().get_connections();

            btn_update.Click += Btn_update_Click;
        }

        private void Cb_database_SelectionChanged(object sender,SelectionChangedEventArgs e) {

            ComboBox cb_onnection = (ComboBox)sender;
            String connection_string = (String)cb_onnection.SelectedValue;

            if((_session = hibernate_util.get_instance().get_session(connection_string)) != null) {

                List<pe_ip> ips = _session.CreateCriteria<pe_ip>().List<pe_ip>().OrderBy(x => x.no_ip).ToList();
                ips = (from ip in ips where ip.pe_grmu_list.Count > 0 orderby ip.no_ip ascending select ip).ToList();

                tree_main.ItemsSource = new ObservableCollection<pe_ip>(ips);
            }
        }

        private void copy_libl(pe_libl libl_to_copy) {

            ITransaction transaction = null;
            try {
                pe_attr attr = tree_main.SelectedValue as pe_attr;
                if(attr != null) {
                    transaction = _session.BeginTransaction();

                    pe_libl libl = libl_to_copy.shallow_copy();
                    libl.no_ip = attr.pe_muta.no_ip;

                    // suppprimer l'ancien libellé et le remplacer par la copie
                    pe_libl libl_ = attr.pe_libl_list.First();
                    if(libl_ != null) {
                        attr.pe_muta.pe_ip.pe_libl_list.Remove(libl_);
                        _session.Delete(libl_);
                    }

                    _session.Save(libl);
                    transaction.Commit();

                    _session.Refresh(attr.pe_muta.pe_ip);
                    var libl_list = attr.pe_libl_list;

                }
            } catch(Exception exception) {
                if(transaction != null) {
                    if(transaction != null) {
                        transaction.Rollback();
                    }
                    MessageBox.Show(exception.StackTrace);
                }
            }
        }

        private void Btn_update_Click(object sender, RoutedEventArgs e) {

            new persistant_controller(_session).update(g_detail.DataContext);
        }

        #region GRMU

        private void add_grmu_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }


        private void add_grmu_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_ip ip = e.Parameter as pe_ip;
            if(ip != null) {
                pe_grmu grmu = new pe_grmu {
                    no_ip = ip.no_ip,
                    pe_grmu_id = hibernate_util.get_instance().generate_grmu_id()
                };

                new persistant_controller(_session).add_child(ip, grmu);
            }
        }

        private void re_order_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void re_order_muta_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_grmu grmu = e.Parameter as pe_grmu;
            if(grmu != null) {
                int i = 1;
                foreach(var muta in grmu.pe_muta_list.OrderBy(x => x.muta_order)) {
                    muta.muta_order = i;
                    i++;
                }
            }
        }

        #endregion

        #region MUTATION

        private void copy_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = _muta == null;
        }

        private void copy_muta_executed(object sender, ExecutedRoutedEventArgs e) {

            _muta = e.Parameter as pe_muta;
        }
        private void paste_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = _muta != null;
        }

        private void paste_muta_executed(object sender, ExecutedRoutedEventArgs e) {

            persistant_controller persistant_controller = new persistant_controller(_session);

            if(_muta != null) {
                pe_muta muta = null;
                pe_grmu grmu = e.Parameter as pe_grmu;

                // TODO : à compléter  
                // copie d'une mutation pour la même ip => seule la référence est ajoutée (pas de copie effectuée)

                if(!_muta.pe_ip.Equals(grmu.pe_ip)) {
                    muta = _muta.deep_copy(hibernate_util.get_instance().generate_muta_id(),grmu.pe_ip);
                    persistant_controller.update(muta);
                }

                pe_gmmu gmmu = new pe_gmmu(grmu, muta ?? _muta);
                persistant_controller.update(gmmu);

                _muta = null;

                TreeViewItem source = e.OriginalSource as TreeViewItem;
                source.ItemsSource = null;
                source.ItemsSource = dg_list.ItemsSource = grmu.pe_muta_list;
            }
        }

        private void delete_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void delete_muta_executed(object sender, ExecutedRoutedEventArgs e) {

            persistant_controller persistant_controller = new persistant_controller(_session);

            TreeViewItem source = e.OriginalSource as TreeViewItem;
            pe_muta muta = source.DataContext as pe_muta;

            // comme gmmu ne fait pas partie de l'arbre (pour des questions de visibilité)
            // le grmu n'est accessible que par l'élément parent de la mutation 

            TreeViewItem parent = GetSelectedTreeViewItemParent(source) as TreeViewItem;
            pe_grmu grmu = parent.DataContext as pe_grmu;
            pe_gmmu gmmu = muta.pe_gmmu_list.Where(x => x.pe_grmu.Equals(grmu)).Single();
            
            persistant_controller.delete(null, gmmu);
            persistant_controller.delete(null, muta);
            
            parent.ItemsSource = null;
            parent.ItemsSource = grmu.pe_muta_list;
        }

        private void re_order_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void re_order_attr_executed(object sender, ExecutedRoutedEventArgs e) {

            TreeViewItem source = e.OriginalSource as TreeViewItem;

            int i = 1;
            foreach(var item in source.Items) {
                var attr = item as pe_attr;
                if(attr != null) {
                    attr.position = i;
                    i++;
                }
            }
        }

        #endregion

        #region ATTRIBUT

        private void copy_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = _attr == null;
        }

        private void copy_attr_executed(object sender, ExecutedRoutedEventArgs e) {

            _attr = e.Parameter as pe_attr;
        }

        private void paste_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = _attr != null;
        }

        private void paste_attr_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_muta muta = e.Parameter as pe_muta;
            new persistant_controller(_session).add_child(muta, _attr.shallow_copy(muta));
            _attr = null;

            TreeViewItem source = e.OriginalSource as TreeViewItem;
            source.ItemsSource = null;
            source.ItemsSource = muta.pe_attr_list;
            dg_list.ItemsSource = muta.pe_attr_list;
        }

        private void add_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void add_attr_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_muta muta = e.Parameter as pe_muta;

            var global_dict = new global_dict() {
                dict_list = hibernate_util.get_instance().get_attr_dict_list(),
                dict_list_name = "DICTIONNAIRE DES ATTRIBUTS"
            };

            var w_dict = new w_generic();
            w_dict.Title = "AJOUTER UN ATTRIBUT A LA MUTATION";

            w_dict.cb_connection.Visibility = Visibility.Collapsed;

            w_dict._muta = muta;
            w_dict._session = _session;

            w_dict.tree_main.ItemsSource = CollectionViewSource.GetDefaultView(new List<global_dict>() { global_dict });
            w_dict.ShowDialog();

            new persistant_controller(_session).add_child(muta, w_dict._attr);

            TreeViewItem source = e.OriginalSource as TreeViewItem;
            source.ItemsSource = null;
            source.ItemsSource = muta.pe_attr_list;
            dg_list.ItemsSource = muta.pe_attr_list;
        }

        private void delete_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void delete_attr_executed(object sender, ExecutedRoutedEventArgs e) {

            TreeViewItem source = e.OriginalSource as TreeViewItem;
            pe_attr attr = source.DataContext as pe_attr;
            pe_muta muta = attr.pe_muta;

            new persistant_controller(_session).delete(muta, attr);

            TreeViewItem parent = GetSelectedTreeViewItemParent(source) as TreeViewItem;
            parent.ItemsSource = null;
            parent.ItemsSource = muta.pe_attr_list;
        }

        #endregion
             
        #region LIBL

        private void copy_libl_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = _libl != null;
            } else {
                e.CanExecute = _libl == null;
            }
        }
        private void copy_libl_executed(object sender, ExecutedRoutedEventArgs e) {

            if(!e.Parameter.ToString().Equals("coller_element")) {
                pe_libl parameter = e.Parameter as pe_libl;
                if(parameter != null) {
                    _libl = parameter;
                }
            } else if(_libl != null) {
                copy_libl(_libl);
                _libl = null;
            }
        }

        #endregion

        #region DICT

        private void dict_select_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = (_muta != null);
        }

        private void dict_select_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_dict dict = e.Parameter as pe_dict;
            if(dict != null) {
                _attr = new pe_attr() {
                    nom_attr = dict.nom_dict,
                    clatit_attr = dict.clatit_dict,
                };

                this.Close();
            }
        }

        private void dict_add_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void dict_add_executed(object sender, ExecutedRoutedEventArgs e) {

            pe_dict dict = new pe_dict() { pe_dict_id = hibernate_util.get_instance().generate_dict_id() };
            new persistant_controller(hibernate_util.get_instance().get_current_session()).update(dict);
        }

        #endregion

        #region TREE_VIEW

        private void tree_view_selected(object sender, RoutedEventArgs e) {

            pe_muta muta = tree_main.SelectedValue as pe_muta;
            if(muta != null) {
                TreeViewItem source = e.OriginalSource as TreeViewItem;
                if(source != null) {
                    source.ItemsSource = null;
                    source.ItemsSource = muta.pe_attr_list;
                }
            }

            pe_grmu grmu = tree_main.SelectedValue as pe_grmu;
            if(grmu != null) {
                TreeViewItem source = e.OriginalSource as TreeViewItem;
                if(source != null) {
                    source.ItemsSource = null;
                    source.ItemsSource = grmu.pe_muta_list;
                }
            }
        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item) {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while(!(parent is TreeViewItem || parent is TreeView)) {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }

        #endregion
    }
}


