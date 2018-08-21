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

namespace Soldel.Views {

    /// <summary>
    /// Logique d'interaction pour w_generic.xaml
    /// </summary>
    public partial class w_generic : Window {
        private pe_attr clip_attr;
        private pe_libl clip_libl;

        private ISession session;

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
            session = hibernate_util.get_instance().get_session(connectionString);

            List<pe_grmu> grmus = session.CreateCriteria<pe_grmu>().List<pe_grmu>().ToList();
            var object_list = (from grmu in grmus orderby grmu.no_ip ascending select grmu).ToList();

            build_tree(object_list.ToList<object>());
        }

        private void build_tree(List<object> objects) {
            tree_main.ItemsSource = CollectionViewSource.GetDefaultView(objects);
        }

        // TODO : généralisation dès que les autres élément de l'arbre seront pris en compte

        private void Btn_tree_add_Click(object sender,RoutedEventArgs e) {

            ITransaction transaction = null;
            try {
                transaction = session.BeginTransaction();
                if(this.tree_main.SelectedValue is pe_grmu) {
                    pe_grmu selectedValue = tree_main.SelectedValue as pe_grmu;
                    pe_muta muta = new pe_muta {
                        no_ip = selectedValue.no_ip,
                        pe_muta_id = generate_muta_id()
                    };

                    session.Save(muta);
                    pe_gmmu _gmmu = new pe_gmmu(selectedValue,muta);
                    session.Save(_gmmu);
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

                    string str = generate_muta_id();
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
                pe_muta muta = this.tree_main.SelectedValue as pe_muta;
                if(muta != null) {
                    transaction = session.BeginTransaction();

                    pe_attr attr = attr_to_copy.shallow_copy(muta);
                    muta.add_attr(attr);
                    session.Save(muta);
                    session.Refresh(muta);

                    transaction.Commit();
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
                    attr.pe_muta.pe_ip.pe_libl_list.Remove(libl_);
                    session.Delete(libl_);

                    session.Save(libl);
                    transaction.Commit();

                    session.Refresh(attr.pe_muta.pe_ip);
                    var libl_list = attr.pe_libl_list;

                    // TODO : améliorer pour ne rafraichir que la partie concernéeS
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

        private string generate_muta_id() {
            return session.CreateSQLQuery("SELECT MAX (to_number(pe_muta_id)) + 1 from pe_muta").UniqueResult().ToString();
        }

        //---------------------------------------------------------------------
        // Command HANDLER
        //---------------------------------------------------------------------
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
    }
}

