using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using mupeModel.Utils;
using NHibernate;

namespace Soldel.Views {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class uc_select_connection:UserControl {
        public ISession session { get; set; }
        public uc_select_connection() {
            InitializeComponent();

            cb_connection.ItemsSource = hibernate_util.get_instance().get_connections();
            cb_connection.SelectionChanged += Cb_connection_SelectionChanged;
        }

        private void Cb_connection_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            ComboBox cbConnection = (ComboBox)sender;
            String connectionString = (String)cbConnection.SelectedValue;
            session = hibernate_util.get_instance().get_session(connectionString);
        }
    }
}
