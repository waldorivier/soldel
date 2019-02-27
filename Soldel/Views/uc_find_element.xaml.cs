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
using mupeModel;
using System.Data;

namespace Soldel.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class uc_find_element : UserControl
    {
        // public ISession session { get; set; }
        public uc_find_element()
        {
            InitializeComponent();

            uc_select_connection.AddHandler(ListBox.SelectionChangedEvent, new RoutedEventHandler(Cb_connection_list_SelectionChanged));
        }

        private void Cb_connection_list_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(uc_select_connection.session != null)
            {
                List<pe_elem> list = uc_select_connection.session.CreateCriteria<pe_elem>().List<pe_elem>().ToList();
                // cb_element.ItemsSource = CollectionViewSource.GetDefaultView(list);
            }

        }
    }
}
