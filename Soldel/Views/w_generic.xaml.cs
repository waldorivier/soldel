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

namespace Soldel.Views {

    /// <summary>
    /// Logique d'interaction pour w_generic.xaml
    /// </summary>
    public partial class w_generic:Window {
        private pe_attr clip_attr;
        private pe_libl clip_libl;

        private ISession session;
        private bool test_dictionary = true;

        public w_generic() {
            InitializeComponent();

            Clipboard.Clear();

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = hibernate_util.get_instance().get_connections();

            btn_tree_add.Click += Btn_tree_add_Click;
            btn_tree_delete.Click += Btn_tree_delete_Click;
            btn_update.Click += Btn_update_Click;
        }

        private void Cb_database_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            ComboBox cbConnection = (ComboBox)sender;
            String connectionString = (String)cbConnection.SelectedValue;

            if((session = hibernate_util.get_instance().get_session(connectionString)) != null) {

                if(!test_dictionary) {
                    List<pe_ip> ips = session.CreateCriteria<pe_ip>().List<pe_ip>().OrderBy(x => x.no_ip).ToList();
                    ips = (from ip in ips where ip.pe_grmu_list.Count > 0 orderby ip.no_ip ascending select ip).ToList();

                    tree_main.ItemsSource = CollectionViewSource.GetDefaultView(ips);
                } else {
                    var global = new global() {
                        dict_list = session.CreateCriteria<pe_dict>().List<pe_dict>().OrderBy(x => x.nom_dict).ToList(),
                        dict_list_name = "Dictionnaire des attributs"
                    };

                    tree_main.ItemsSource = CollectionViewSource.GetDefaultView(new List<global>() { global });
                }
            }
        }

        // TODO : généralisation dès que les autres élément de l'arbre seront pris en compte

        private void Btn_tree_add_Click(object sender,RoutedEventArgs e) {
            ITransaction transaction = null;

            try {
                transaction = session.BeginTransaction();

                var selected = tree_main.SelectedValue;
                if(selected != null) {
                    if(selected is pe_ip) {
                        var ip = selected as pe_ip;

                        pe_grmu grmu = new pe_grmu {
                            no_ip = ip.no_ip,
                            pe_grmu_id = hibernate_util.get_instance().generate_grmu_id()
                        };

                        session.Save(grmu);

                    } else if(selected is pe_muta) {
                        var muta = selected as pe_muta;

                        // pe_attr attr = new pe_attr { nom_attr = "TXACTR"};
                        // muta.add_attr(attr);
                        // session.Save(muta);

                    } else if(selected is global) {

                        // en l'état, il n'y a que le dictionnaire des attributs        
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
                    transaction = session.BeginTransaction();

                    pe_ip ip = session.Get<pe_ip>(grmu.no_ip);
                    pe_muta muta_to_copy = session.Get<pe_muta>(element_id);

                    string str = hibernate_util.get_instance().generate_muta_id();
                    pe_muta muta = muta_to_copy.deep_copy(str,ip);

                    ip.add_muta(muta);
                    pe_gmmu gmmu = new pe_gmmu(grmu, muta);
                    session.Save(gmmu);

                    session.Save(ip);

                    transaction.Commit();
                    session.Refresh(gmmu);
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

        private void copy_attr(pe_attr attr_to_copy) {
            ITransaction transaction = null;
            try {
                pe_muta muta = tree_main.SelectedValue as pe_muta;
                if(muta != null) {
                    transaction = session.BeginTransaction();

                    pe_attr attr = attr_to_copy.shallow_copy(muta);
                    muta.add_attr(attr);
                    session.Save(muta);

                    transaction.Commit();
                    session.Refresh(muta);
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

        private void copy_libl(pe_libl libl_to_copy) {
            ITransaction transaction = null;
            try {
                pe_attr attr = tree_main.SelectedValue as pe_attr;
                if(attr != null) {
                    transaction = session.BeginTransaction();

                    pe_libl libl = libl_to_copy.shallow_copy();
                    libl.no_ip = attr.pe_muta.no_ip;

                    // suppprimer l'ancien libellé et le remplacer par la copie
                    pe_libl libl_ = attr.pe_libl_list.First();
                    if(libl_ != null) { 
                        attr.pe_muta.pe_ip.pe_libl_list.Remove(libl_);
                        session.Delete(libl_);
                    }

                    session.Save(libl);
                    transaction.Commit();

                    session.Refresh(attr.pe_muta.pe_ip);
                    var libl_list = attr.pe_libl_list;

                    // TODO : améliorer pour ne rafraichir que la partie concernée
                    // tree_main.Items.Refresh();
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
                    transaction = session.BeginTransaction();

                    // TODO : ce n'est pas complet, car il faut choisir un des gmmus.....!

                    var gmmu = muta.pe_gmmu_list[0];
                    gmmu.pe_grmu.pe_gmmu_list.Remove(gmmu);
                    muta.pe_gmmu_list.Remove(gmmu);

                    session.Delete(gmmu);
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
                    transaction = session.BeginTransaction();
                    session.Save(g_detail.DataContext);
                    transaction.Commit();
                } catch(Exception ex) {
                    if(transaction != null)
                        transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region COMMAND HANDLER

        private void copy_muta_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            object data = Clipboard.GetData("String");
            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = data != null;
            } else {
                e.CanExecute = data == null;
            }
        }

        private void copy_muta_executed(object sender, ExecutedRoutedEventArgs e) {
            if(!e.Parameter.ToString().Equals("coller_element")) {
                Clipboard.SetData("String",e.Parameter.ToString());
            } else {
                string data = Clipboard.GetData("String") as string;
                copy_muta(data);
                Clipboard.Clear();
            }
        }

        private void copy_attr_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = clip_attr != null;
            } else {
                e.CanExecute = clip_attr == null;
            }
        }

        private void copy_attr_executed(object sender, ExecutedRoutedEventArgs e) {
            if(!e.Parameter.ToString().Equals("coller_element")) {
                pe_attr parameter = e.Parameter as pe_attr;
                if(parameter != null) {
                    clip_attr = parameter;
                }
            } else if(clip_attr != null) {
                copy_attr(clip_attr);
                clip_attr = null;
            }
        }

        private void copy_libl_can_execute(object sender,CanExecuteRoutedEventArgs e) {
            if(e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = clip_libl != null;
            } else {
                e.CanExecute = clip_libl == null;
            }
        }
        private void copy_libl_executed(object sender,ExecutedRoutedEventArgs e) {
            if(!e.Parameter.ToString().Equals("coller_element")) {
                pe_libl parameter = e.Parameter as pe_libl;
                if(parameter != null) {
                    clip_libl = parameter;
                }
            } else if(clip_libl != null) {
                copy_libl(clip_libl);
                clip_libl = null;
            }
        }

        private void add_attr_can_execute(object sender,CanExecuteRoutedEventArgs e) {
            e.CanExecute = clip_libl == null;
        }

        private void add_attr_executed(object sender,ExecutedRoutedEventArgs e) {
            List<pe_dict> dicts = session.CreateCriteria<pe_dict>().List<pe_dict>().OrderBy(x => x.nom_dict).ToList();

            var w_dict = new w_generic();

            w_dict.tree_main.ItemsSource = CollectionViewSource.GetDefaultView(dicts);
            w_dict.ShowDialog();
        }

        #endregion
    }
}

