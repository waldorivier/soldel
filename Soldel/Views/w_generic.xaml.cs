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

        private pe_attr _clip_attr;
        private pe_libl _clip_libl;
        private pe_muta _clip_muta;

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
                tree_main.ItemsSource = CollectionViewSource.GetDefaultView(ips);
            }
        }

        // TODO : généralisation dès que les autres élément de l'arbre seront pris en compte (en cours)

        private void Btn_tree_add_Click(object sender,RoutedEventArgs e) {

            ITransaction transaction = null;

            try {
                transaction = _session.BeginTransaction();

                var selected = tree_main.SelectedValue;
                if(selected != null) {
                    if(selected is pe_ip) {
                        var ip = selected as pe_ip;

                        pe_grmu grmu = new pe_grmu {
                            no_ip = ip.no_ip,
                            pe_grmu_id = hibernate_util.get_instance().generate_grmu_id()
                        };

                        _session.Save(grmu);

                    } else if(selected is pe_muta) {
                        var muta = selected as pe_muta;

                        // pe_attr attr = new pe_attr { nom_attr = "TXACTR"};
                        // muta.add_attr(attr);
                        // session.Save(muta);

                    } else if(selected is global_dict) {

                        // actuellement il n'y a que le dictionnaire des attributs        
                        new chatbot_box().ShowDialog();
                    }

                    transaction.Commit();
                    tree_main.Items.Refresh();
                }
            } catch(Exception exception) {
                if(transaction != null) {
                    if(transaction != null) {
                        transaction.Rollback();
                    }
                    MessageBox.Show(exception.Message);
                }
            }
        }

        // TODO : encapsuler de manière générique
        private void copy_muta(string element_id) {

            ITransaction transaction = null;

            try {
                var grmu = tree_main.SelectedValue as pe_grmu;
                if(grmu != null) {
                    transaction = _session.BeginTransaction();

                    pe_ip ip = _session.Get<pe_ip>(grmu.no_ip);
                    pe_muta muta_to_copy = _session.Get<pe_muta>(element_id);

                    string str = hibernate_util.get_instance().generate_muta_id();
                    pe_muta muta = muta_to_copy.deep_copy(str,ip);

                    ip.add_muta(muta);
                    pe_gmmu gmmu = new pe_gmmu(grmu,muta);
                    _session.Save(gmmu);

                    _session.Save(ip);

                    transaction.Commit();
                    _session.Refresh(gmmu);

                    tree_main.Items.Refresh();
                }
            } catch(Exception ex) {
                if(transaction != null) {
                    if(transaction != null)
                        transaction.Rollback();

                    MessageBox.Show(ex.StackTrace);
                }
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

                    tree_main.Items.Refresh();
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

        //---------------------------------------------------------------------
        // TODO : encapsuler de manière générique
        // on ne supprime que la relation
        //---------------------------------------------------------------------
        private void Btn_tree_delete_Click(object sender,RoutedEventArgs e) {

            ITransaction transaction = null;

            try {
                var muta = tree_main.SelectedValue as pe_muta;
                if(muta != null) {
                    transaction = _session.BeginTransaction();

                    // TODO : ce n'est pas complet, car il faut choisir un des gmmus.....!

                    var gmmu = muta.pe_gmmu_list[0];
                    gmmu.pe_grmu.pe_gmmu_list.Remove(gmmu);
                    muta.pe_gmmu_list.Remove(gmmu);

                    _session.Delete(gmmu);
                    transaction.Commit();
                    tree_main.Items.Refresh();
                }
            } catch(Exception ex) {
                if(transaction != null) {
                    if(transaction != null)
                        transaction.Rollback();

                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Btn_update_Click(object sender,RoutedEventArgs e) {

            ITransaction transaction = null;
            if(g_detail.DataContext != null) {
                try {
                    transaction = _session.BeginTransaction();
                    _session.Save(g_detail.DataContext);
                    transaction.Commit();
                } catch(Exception ex) {
                    if(transaction != null)
                        transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region ATTRIBUT


        private void copy_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {

            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = _clip_attr != null;
            } else {
                e.CanExecute = _clip_attr == null;
            }
        }

        private void copy_attr_executed(object sender,ExecutedRoutedEventArgs e) {

            if(!e.Parameter.ToString().Equals("coller_element")) {
                pe_attr parameter = e.Parameter as pe_attr;
                if(parameter != null) {
                    _clip_attr = parameter;
                }
            } else if(_clip_attr != null) {
                pe_muta muta = tree_main.SelectedValue as pe_muta;
                if(muta != null) {
                    new persistant_controller(_session).add_child(muta, _clip_attr.shallow_copy(muta));
                    tree_main.Items.Refresh();

                    _clip_attr = null;
                }
            }
        }

        private void add_attr_can_execute(object sender,CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void add_attr_executed(object sender,ExecutedRoutedEventArgs e) {

            pe_muta muta = e.Parameter as pe_muta;
            if(muta != null) {
                var global_dict = new global_dict() {
                    dict_list = _session.CreateCriteria<pe_dict>().List<pe_dict>().OrderBy(x => x.nom_dict).ToList(),
                    dict_list_name = "Dictionnaire des attributs"
                };

                var w_dict = new w_generic();
                w_dict.Title = "AJOUTER UN ATTRIBUT A LA MUTATION";
                w_dict.cb_connection.Visibility = Visibility.Collapsed;

                w_dict._clip_muta = muta;
                w_dict._session = _session;

                w_dict.tree_main.ItemsSource = CollectionViewSource.GetDefaultView(new List<global_dict>() { global_dict });
                w_dict.ShowDialog();

                // tree_main.Items.Refresh();
             }
        }

        private void delete_attr_can_execute(object sender,CanExecuteRoutedEventArgs e) {

            e.CanExecute = true;
        }

        private void delete_attr_executed(object sender,ExecutedRoutedEventArgs e) {

            pe_attr attr = e.Parameter as pe_attr;
            if(attr != null) {
                new persistant_controller(_session).delete(attr.pe_muta,attr);
                tree_main.Items.Refresh();
            }
        }

        #endregion

        #region MUTATION

        private void copy_muta_can_execute(object sender,CanExecuteRoutedEventArgs e) {

            object data = Clipboard.GetData("String");
            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = data != null;
            } else {
                e.CanExecute = data == null;
            }
        }

        private void copy_muta_executed(object sender,ExecutedRoutedEventArgs e) {

            if(!e.Parameter.ToString().Equals("coller_element")) {
                Clipboard.SetData("String",e.Parameter.ToString());
            } else {
                string data = Clipboard.GetData("String") as string;
                copy_muta(data);
                Clipboard.Clear();
            }
        }

        #endregion

        #region LIBL

        private void copy_libl_can_execute(object sender,CanExecuteRoutedEventArgs e) {

            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = _clip_libl != null;
            } else {
                e.CanExecute = _clip_libl == null;
            }
        }
        private void copy_libl_executed(object sender,ExecutedRoutedEventArgs e) {

            if(!e.Parameter.ToString().Equals("coller_element")) {
                pe_libl parameter = e.Parameter as pe_libl;
                if(parameter != null) {
                    _clip_libl = parameter;
                }
            } else if(_clip_libl != null) {
                copy_libl(_clip_libl);
                _clip_libl = null;
            }
        }

        #endregion

        #region DICT

        private void dict_select_can_execute(object sender,CanExecuteRoutedEventArgs e) {

            e.CanExecute = (_clip_muta != null);
        }

        delegate void add_child(object child);

        private void dict_select_executed(object sender,ExecutedRoutedEventArgs e) {

            pe_dict dict = e.Parameter as pe_dict;
            if(dict != null) {
                pe_attr attr = new pe_attr();

                attr.nom_attr = dict.nom_dict;
                attr.clatit_attr = dict.clatit_dict;

                if(_clip_muta.can_add_child(attr)) {
                    new persistant_controller(_session).add_child(_clip_muta,attr);

                    this.Close();
                }
            }
        }

        #endregion
    }
}


