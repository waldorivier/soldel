using System;
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
using mupeModel.Views.Converters;

namespace Soldel.Views {
        
    /// <summary>
    /// Logique d'interaction pour w_generic.xaml
    /// </summary>
    public partial class w_generic : Window {

        private TreeViewItem _tree_view_item;

        private pe_attr _attr;
        private pe_libl _libl;
        public  pe_muta _muta;
        public  pe_grmu _grmu;

        persistant_controller _persistant_controller;

        public w_generic() {

            InitializeComponent();
            Clipboard.Clear();

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = hibernate_util.get_instance().get_connections();

            btn_update.Click += Btn_update_Click;
        }

        private void Cb_database_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ISession session;
            ComboBox cb_onnection = (ComboBox)sender;
            String connection_string = (String)cb_onnection.SelectedValue;

            if ((session = hibernate_util.get_instance().get_session(connection_string)) != null) {
                _persistant_controller = new persistant_controller(session);

                List<pe_ip> ips = session.CreateCriteria<pe_ip>().List<pe_ip>().OrderBy(x => x.no_ip).ToList();
                ips = (from ip in ips where ip.pe_grmu_list.Count > 0 orderby ip.no_ip ascending select ip).ToList();
                // ips = (from ip in ips where ip.no_ip.Equals(11) select ip).ToList();

                tree_main.ItemsSource = new ObservableCollection<pe_ip>(ips);
            }
        }

        private void copy_libl(pe_libl libl_to_copy) {
            ITransaction transaction = null;
            try {
                ISession session = _persistant_controller.session;

                pe_attr attr = tree_main.SelectedValue as pe_attr;
                if (attr != null) {
                    transaction = session.BeginTransaction();

                    pe_libl libl = libl_to_copy.shallow_copy();
                    libl.no_ip = attr.pe_muta.no_ip;

                    // suppprimer l'ancien libellé et le remplacer par la copie
                    pe_libl libl_ = attr.pe_libl_list.First();
                    if (libl_ != null) {
                        attr.pe_muta.pe_ip.pe_libl_list.Remove(libl_);
                        session.Delete(libl_);
                    }

                    session.Save(libl);
                    transaction.Commit();

                    session.Refresh(attr.pe_muta.pe_ip);
                    var libl_list = attr.pe_libl_list;

                }
            } catch (Exception exception) {
                if (transaction != null) {
                    if (transaction != null) {
                        transaction.Rollback();
                    }
                    MessageBox.Show(exception.StackTrace);
                }
            }
        }

        private void Btn_update_Click(object sender, RoutedEventArgs e) {
           _persistant_controller.update(g_detail.DataContext);
        }

        #region GRMU

        private void add_grmu_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void add_grmu_executed(object sender, ExecutedRoutedEventArgs e) {
            pe_ip ip = e.Parameter as pe_ip;
            if (ip != null) {
                pe_grmu grmu = new pe_grmu {
                    no_ip = ip.no_ip,
                    pe_grmu_id = hibernate_util.get_instance().generate_grmu_id()
                };

                _persistant_controller.add_child(ip, grmu);
            }
        }

        private void copy_grmu_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = _grmu == null;
        }

        private void copy_grmu_executed(object sender, ExecutedRoutedEventArgs e) {
            _grmu = e.Parameter as pe_grmu;
        }

        private void paste_grmu_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = _grmu != null;
        }

        private void paste_grmu_executed(object sender, ExecutedRoutedEventArgs e) {
            if (_grmu != null) {
                pe_ip ip = e.Parameter as pe_ip;

                var grmu_c = _grmu.shallow_copy(hibernate_util.get_instance().generate_grmu_id(), ip);
                _persistant_controller.add_child(ip, grmu_c);

                // copie de la configuration issue de la même ip; les mutations ne sont pas copiées mais
                // seule une référence est ajoutée
                if (_grmu.pe_ip.Equals(ip)) {
                    foreach (var muta in _grmu.pe_muta_list) {
                        pe_gmmu gmmu = new pe_gmmu(grmu_c, muta);
                        _persistant_controller.update(gmmu);
                    }
                } else {
                    var muta_id = Int32.Parse(hibernate_util.get_instance().generate_muta_id());
                    foreach (var muta in _grmu.pe_muta_list) {
                        var muta_c = muta.deep_copy(muta_id.ToString(), grmu_c.pe_ip);
                        _persistant_controller.update(muta_c);

                        pe_gmmu gmmu = new pe_gmmu(grmu_c, muta_c);
                        _persistant_controller.update(gmmu);
                        muta_id++;
                    }
                }
                _persistant_controller.session.Refresh(grmu_c);
            }
            _grmu = null;
        }

        private void re_order_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void re_order_muta_executed(object sender, ExecutedRoutedEventArgs e) {
            pe_grmu grmu = e.Parameter as pe_grmu;
            if (grmu != null) {
                int i = 1;
                foreach (var muta in grmu.pe_muta_list.OrderBy(x => x.pe_muta_id)) {
                    muta.muta_order = i;
                    i++;
                }
            }
        }

        #endregion

        #region MUTATION

        public class muta_action {
            private pe_muta _muta = null;
            private pe_grmu _grmu = null;
            private persistant_controller _persistant_controller = null;

            public Action action_1;
            public Action action_2;

            public pe_muta muta { get => _muta; set => _muta = value; }
            internal persistant_controller persistant_controller { get => _persistant_controller; set => _persistant_controller = value; }

            public muta_action(pe_grmu grmu, pe_muta muta) {
                _muta = muta;
                _grmu = grmu;

                action_1 = paste_muta_deep;
                action_2 = paste_muta_reference;
            }

            private void paste_muta_deep() {
                _muta = _muta.deep_copy(hibernate_util.get_instance().generate_muta_id(), _grmu.pe_ip);
                _persistant_controller.update(_muta);
            }

            private void paste_muta_reference() {
            }
        }

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
            if (_muta != null) {
                pe_grmu grmu = e.Parameter as pe_grmu;

                muta_action a = new muta_action(grmu, _muta);
                a.persistant_controller = _persistant_controller;

                // copie d'une mutation pour la même ip => seule la référence est ajoutée (pas de copie effectuée)
                // si l'on ne veut pas ajouter une référence, il faut copier une mutation despuis une autre ip

                var chatbot_box = new chatbot_box(
                    "Faire une copie ou ajouter simplement une référence (ceci n'étant possible " +
                    "que si l'IP source et identique à l'IP destination)", 
                    a.action_1, 
                    a.action_2);

                chatbot_box.ShowDialog();

                pe_gmmu gmmu = new pe_gmmu(grmu, a.muta  ?? _muta);
                _persistant_controller.update(gmmu);
                _persistant_controller.session.Refresh(grmu);

                // provoque une mise à jour de la liste ! 
                grmu.pe_muta_list = null;
                _muta = null;
            }
        }

        private void delete_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void delete_muta_executed(object sender, ExecutedRoutedEventArgs e) {
            TreeViewItem source = e.OriginalSource as TreeViewItem;
            pe_muta muta = source.DataContext as pe_muta;

            // REM : étant donné que gmmu n'est pas visible dans l'arbre (pour alléger la représentation)
            // le grmu n'est accessible que par l'élément vue (view) parent de la mutation 

            TreeViewItem parent = get_selected_tree_view_item_parent(source) as TreeViewItem;
            pe_grmu grmu = parent.DataContext as pe_grmu;
            pe_gmmu gmmu = muta.pe_gmmu_list.Where(x => x.pe_grmu.Equals(grmu)).Single();
                        
            // auncun de deux "parents" n'est actualisé : situation à remédier
            _persistant_controller.delete(null, gmmu);
            _persistant_controller.delete(null, muta);
        }

        private void re_order_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void re_order_attr_executed(object sender, ExecutedRoutedEventArgs e) {
            TreeViewItem source = e.OriginalSource as TreeViewItem;

            int i = 1;
            foreach (var item in source.Items) {
                var attr = item as pe_attr;
                if (attr != null) {
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
            _persistant_controller.add_child(muta, _attr.shallow_copy(muta));
            _attr = null;
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
            w_dict._persistant_controller = _persistant_controller;
            w_dict.Title = "AJOUTER UN ATTRIBUT A LA MUTATION";
            w_dict.cb_connection.Visibility = Visibility.Collapsed;

            w_dict._muta = muta;
            w_dict.tree_main.ItemsSource = CollectionViewSource.GetDefaultView(new List<global_dict>() { global_dict });
            w_dict.ShowDialog();

            _persistant_controller.add_child(muta, w_dict._attr);
        }

        private void delete_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void delete_attr_executed(object sender, ExecutedRoutedEventArgs e) {
            pe_attr attr = e.Parameter as pe_attr;
            pe_muta muta = attr.pe_muta;

            _persistant_controller.delete(muta, attr);
        }

        #endregion

        #region LIBL

        private void add_libl_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            pe_attr attr = e.Parameter as pe_attr;
            if (attr != null) {
                e.CanExecute = attr.pe_libl_list.Count == 0;
            }   
        }

        private void add_libl_executed(object sender, ExecutedRoutedEventArgs e) {
            pe_attr attr = e.Parameter as pe_attr;

            pe_libl libl = new pe_libl();
            libl.nom_attr = attr.nom_attr;
            _persistant_controller.add_child(attr.pe_muta.pe_ip, libl);
        }

        private void copy_libl_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            if (e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = _libl != null;
            } else {
                e.CanExecute = _libl == null;
            }
        }
        private void copy_libl_executed(object sender, ExecutedRoutedEventArgs e) {
            if (!e.Parameter.ToString().Equals("coller_element")) {
                pe_libl parameter = e.Parameter as pe_libl;
                if (parameter != null) {
                    _libl = parameter;
                }
            } else if (_libl != null) {
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
            if (dict != null) {
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
            _persistant_controller.update(dict);
        }

        #endregion

        #region TREE_VIEW

        private void tree_view_selected_item_changed(object sender, RoutedPropertyChangedEventArgs<object> e) {
            TreeViewItem current_tree_view_item = this.Tag as TreeViewItem;
            pe_grmu grmu = e.NewValue as pe_grmu;
            if (grmu != null) {
                if (grmu.pe_muta_list != null) {
                    if (current_tree_view_item != null) {
                        List<object> l = new List<object>();

                        l.AddRange(grmu.pe_muta_list);

                        folder_node folder_grmu = new folder_node();
                        folder_grmu.child_nodes = grmu.pe_cfgd_list;
                        l.Add(folder_grmu);

                        current_tree_view_item.ItemsSource = l;
                    }
                }
                dg_list.ItemsSource = grmu.pe_muta_list;
            }

            pe_muta muta = e.NewValue as pe_muta;
            if (muta != null) {
                current_tree_view_item.ItemsSource = muta.pe_attr_list;
                try {
                    dg_list.ItemsSource = muta.pe_attr_list;

                    // le datagrid provoque une exception lorsque le type de d'objet affiché change
                } catch (Exception ex) {
                    dg_list.ItemsSource = null;
                    dg_list.ItemsSource = muta.pe_attr_list;
                }
            }

            pe_attr attr = e.NewValue as pe_attr;
            if (attr != null) {
                current_tree_view_item.ItemsSource = attr.pe_libl_list;
            }
            
            global_dict dict = e.NewValue as global_dict;
            if (dict != null) {
                current_tree_view_item.ItemsSource = dict.dict_list;
                try {
                    dg_list.ItemsSource = dict.dict_list;

                // le datagrid provoque une exception lors d'un changement du type d'objet contenu
                } catch (Exception ex) {
                    dg_list.ItemsSource = null;
                    dg_list.ItemsSource = dict.dict_list;
                }
            }
        }

        private void tree_view_selected_item(object sender, RoutedEventArgs e) {
            this.Tag = e.OriginalSource;
        }

        private ItemsControl get_selected_tree_view_item_parent(TreeViewItem item) {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView)) {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as ItemsControl;
        }

        #endregion

        #region DATAGRID

        private void dg_list_auto_generating_column(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            DataGrid dg = sender as DataGrid;
            if (dg != null) {
                if (dg.ItemsSource != null) {
                    if (dg.ItemsSource as IList<pe_attr> != null) {
                        if (!pe_attr.columns_to_display.Contains(e.Column.Header)) {
                            e.Cancel = true;
                        }

                        if (pe_attr.columns_read_only.Contains(e.Column.Header)) {
                            e.Column.IsReadOnly = true;
                        }
                    }
                    if (dg.ItemsSource as IList<pe_muta> != null) {
                        if (!pe_muta.columns_to_display.Contains(e.Column.Header)) {
                            e.Cancel = true;
                        }
                    }
                    if (dg.ItemsSource as IList<pe_dict> != null) {
                        if (!pe_dict.columns_to_display.Contains(e.Column.Header)) {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }
        #endregion
    }
}


