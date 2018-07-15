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
        private ISession session;

        public w_generic() {
            InitializeComponent();

            Clipboard.Clear();

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = HibernateUtil.get_instance().get_connections();

            btn_tree_add.Click += Btn_tree_add_Click;
            btn_tree_delete.Click += Btn_tree_delete_Click;
            btn_detail_save.Click += Btn_detail_save_Click;
        }

        private void Cb_database_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox cbConnection = (ComboBox)sender;
            String connectionString = (String)cbConnection.SelectedValue;
            session = HibernateUtil.get_instance().get_session(connectionString);

            List<pe_grmu> grmus = session.CreateCriteria<pe_grmu>().List<pe_grmu>().ToList();
            var object_list = (from grmu in grmus orderby grmu.no_ip ascending select grmu).ToList();

            build_tree(object_list.ToList<object>());
        }

        private void build_tree(List<object> objects) {
            tree_main.ItemsSource = CollectionViewSource.GetDefaultView(objects);
        }

        // TODO : généralisation dès que les autres élément de l'arbre seront pris en compte

        private void Btn_tree_add_Click(object sender, RoutedEventArgs e) {
            ITransaction transaction = null;
            try {
                var grmu = tree_main.SelectedValue as pe_grmu;
                if (grmu != null) {
                    transaction = session.BeginTransaction();

                    var muta = new pe_muta();
                    muta.no_ip = grmu.no_ip;
                    muta.libf_muta =
                    muta.libd_muta =
                    muta.libe_muta =
                    muta.libi_muta = "libelle nouvelle mutation";
                    muta.tyeven = "INDEFINI";

                    // TODO : définir dans un ID GENERATOR
                    muta.pe_muta_id = generate_muta_id();

                    session.Save(muta);
                    var gmmu = new pe_gmmu(grmu, muta);
                    session.Save(gmmu);

                    transaction.Commit();
                    session.Refresh(gmmu);
                    tree_main.Items.Refresh();
                }
            } catch (Exception ex) {
                if (transaction != null) {
                    if (transaction != null)
                        transaction.Rollback();

                    MessageBox.Show(ex.Message);
                }
            }
        }

        // TODO : encapsuler de manière générique
        private void copy_element(string element_id) {
            ITransaction transaction = null;
            
            try {
                var grmu = tree_main.SelectedValue as pe_grmu;
                if (grmu != null) {
                    if (grmu.no_ip == 11) {
                        transaction = session.BeginTransaction();

                        var ip = session.Get<pe_ip>(grmu.no_ip);
                        var muta_to_copy = session.Get<pe_muta>(element_id);

                        var muta_id = generate_muta_id();
                        var muta = muta_to_copy.deep_copy(muta_id, ip);
                        ip.add_muta(muta);

                        var gmmu = new pe_gmmu(grmu, muta);
                        session.Save(gmmu);
                        session.Save(ip);

                        transaction.Commit();
                        session.Refresh(gmmu);
                        tree_main.Items.Refresh();
                    }
                }
            } catch (Exception ex) {
                if (transaction != null) {
                    if (transaction != null)
                        transaction.Rollback();

                    MessageBox.Show(ex.StackTrace);
                }
            }
        }

        //---------------------------------------------------------------------
        // TODO : encapsuler de manière générique
        // on ne supprime que la relation
        //---------------------------------------------------------------------

        private void Btn_tree_delete_Click(object sender, RoutedEventArgs e) {
            ITransaction transaction = null;

            try {
                var muta = tree_main.SelectedValue as pe_muta;
                if (muta != null) {
                    if (muta.no_ip == 11) {
                        transaction = session.BeginTransaction();

                        // TODO : ce n'est pas complet, car il faut choisir un des gmmus.....!

                        var gmmu = muta.pe_gmmu_list[0];
                        gmmu.pe_grmu.pe_gmmu_list.Remove(gmmu);
                        muta.pe_gmmu_list.Remove(gmmu);

                        session.Delete(gmmu);
                        transaction.Commit();
                        tree_main.Items.Refresh();
                    }
                }
            } catch (Exception ex) {
                if (transaction != null) {
                    if (transaction != null)
                        transaction.Rollback();

                    MessageBox.Show(ex.Message);
                }
            }
        }

        // TODO : encapsuler de manière générique
        private void Btn_detail_save_Click(object sender, RoutedEventArgs e) {
            ITransaction transaction = null;
            if (g_detail.DataContext != null) {
                try {
                    transaction = session.BeginTransaction();
                    session.Save(g_detail.DataContext);
                    transaction.Commit();
                } catch (Exception ex) {
                    if (transaction != null)
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

        private void test_can_execute(object sender, CanExecuteRoutedEventArgs e) {
            var id = Clipboard.GetData("String");
            if (e.Parameter.ToString().Equals("coller_element")) {
                e.CanExecute = (id != null);
            } else {
                e.CanExecute = (id == null);
            }
        }

        private void test_executed(object sender, ExecutedRoutedEventArgs e) {
            // copier
            if (!e.Parameter.ToString().Equals("coller_element")) {
                Clipboard.SetData("String", e.Parameter.ToString());

            } else {
                var element_id = Clipboard.GetData("String") as string;
                copy_element(element_id);
                Clipboard.Clear();
            }
        }
    }
}

