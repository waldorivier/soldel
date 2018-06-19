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
using NHibernate;

namespace Soldel.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class uc_select_connection : UserControl
    {
        public ISession session { get; set; }
        public uc_select_connection()
        {
            InitializeComponent();

            cb_connection.ItemsSource = HibernateUtil.getInstance().getConnections;
            cb_connection.SelectionChanged += Cb_connection_SelectionChanged;
            cb_ip_list.DropDownOpened += Cb_ip_list_DropDownOpened;
        }


        private void Cb_connection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbConnection = (ComboBox)sender;
            String connectionString = (String)cbConnection.SelectedValue;
            session = HibernateUtil.getInstance().getSession(connectionString);
        }

        private void Cb_ip_list_DropDownOpened(object sender, EventArgs e)
        {
            List<int> ips = new List<int>() { 4250, 5750 };

            if (session != null) {
                // cb_ip_list.ItemsSource = CollectionViewSource.GetDefaultView(ips);
                cb_ip_list.ItemsSource = session.CreateSQLQuery("SELECT NO_IP FROM PE_IP order by NO_IP asc").List();
            }
        }
    }
}
