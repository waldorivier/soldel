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
        private int noIp = 5750;

        public w_generic() {
            InitializeComponent();

            cb_connection.SelectionChanged += Cb_database_SelectionChanged;
            cb_connection.ItemsSource = HibernateUtil.getInstance().getConnections;
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
    }
}
