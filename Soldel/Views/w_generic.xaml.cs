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

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = HibernateUtil.getInstance().getConnections;

            btn_detail_save.Click += Btn_detail_save_Click;
            btn_tree_add.Click += Btn_tree_add_Click;
        }

        private void Cb_database_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox cbConnection = (ComboBox)sender;
            String connectionString = (String)cbConnection.SelectedValue;
            session = HibernateUtil.getInstance().getSession(connectionString);

            List<pe_grmu> grmus = session.CreateCriteria<pe_grmu>().List<pe_grmu>().ToList();
            var object_list = (from grmu in grmus orderby grmu.no_ip ascending select grmu).ToList();
            buildTree(object_list.ToList<object>());
        }

        private void buildTree(List<object> objects) {
            tree_main.ItemsSource = CollectionViewSource.GetDefaultView(objects);
        }

        private void Btn_tree_add_Click(object sender, RoutedEventArgs e) {
            ITransaction transaction = null;
            try {
                transaction = session.BeginTransaction();

                var grmu = tree_main.SelectedValue as pe_grmu;
                var muta = new pe_muta();
                muta.no_ip = grmu.no_ip;
                session.Save(muta);

                var gmmu = new pe_gmmu(grmu, muta);
                session.Save(grmu);
                transaction.Commit();

                session.Refresh(grmu);
                tree_main.Items.Refresh();

            } catch (Exception ex) {
                if (transaction != null) {
                    transaction.Rollback();
                    var x = ex.StackTrace;
                }
            }
        }

        // TODO : encapsuler vers une fonction générique
        private void Btn_detail_save_Click(object sender, RoutedEventArgs e) {
            ITransaction transaction = null;
            if (dg_detail.DataContext != null) {
                try {
                    transaction = session.BeginTransaction();
                    session.Save(dg_detail.DataContext);
                    transaction.Commit();
                } catch (Exception ex) {
                    if (transaction != null)
                        transaction.Rollback();
                }
            }
        }
    }
}

