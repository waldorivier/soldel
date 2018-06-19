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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class w_elsi : Window {
        private ISession session;
        private Grammar grammar = new Grammar();
        private Parser parser = new Parser();
        private int noIp; 
        public string columnFilterHistory;

        public w_elsi() {
            InitializeComponent();

            uc_select_connection.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Cb_ip_list_SelectionChanged));

            tb_personne.TextChanged += Tb_personne_TextChanged;
            btn_reload_personne.Click += Btn_reload_personne_Click;
            tb_column_filter.LostFocus += Tb_column_filter_LostFocus;
        }

        private void Cb_ip_list_SelectionChanged(object sender, RoutedEventArgs e) {
            if (uc_select_connection.cb_ip_list.SelectedValue != null) {
                noIp = (int)uc_select_connection.cb_ip_list.SelectedValue;
            }
        }

        private void Btn_reload_personne_Click(object sender, RoutedEventArgs e) {
            String nperso = tb_personne.Text;
            if (nperso != null) {
                read_pers_assu_elsi(nperso);
            }
        }
        
        private void Tb_personne_TextChanged(object sender, TextChangedEventArgs e) {
            String nperso = tb_personne.Text;
            if (nperso != null) {
                read_pers_assu_elsi(nperso);
            }
        }

        // lecture de l'ensemble des situations d'une personne d'une IP donnée et actualise les views
        // attachées
        // C'est incomplet et il faut lire les pe_even ainsi que steven qui doit être = 02 (= mutation passée)
        // la relation entre pe_situ et pe_even est se fait via pe_even_id

        private void read_pers_assu_elsi(String nperso) {

            dg_elsi_simple.ItemsSource = null;
            dg_elsi_multiple.ItemsSource = null;

            var assus_a = uc_select_connection.session.CreateCriteria<pe_assu>().Add(Restrictions.Eq("nperso", nperso))
                                               .Add(Restrictions.Eq("no_ip", noIp))
                                               .List<pe_assu>()
                                               .Select(a => a.pe_assu_id).
                                               ToArray<string>();

            List<pe_elsi> elsis = uc_select_connection.session.CreateCriteria<pe_elsi>().Add(Restrictions.In("pe_assu_id", assus_a))
                                                                   .AddOrder(Order.Desc("dtmutx"))
                                                                   .AddOrder(Order.Desc("nositu"))
                                                                   .List<pe_elsi>().ToList();

            if (elsis.Count > 0) {
                List<IDictionary<string, string>> dicts = new List<IDictionary<string, string>>();
                List<TokenTree> tokenTrees = new List<TokenTree>();
                foreach (pe_elsi elsi in elsis) {
                    var elsiTree = new TokenTree();
                    elsiTree.Tokens = parser.parse(new StringBuilder(elsi.liste_elsi), grammar).ToList();
                    elsiTree.build();
                    dicts.Add(elsiTree.asDictionary());
                    tokenTrees.Add(elsiTree);
                }

                // arbitrairement la première mutation de la liste
                dg_elsi_simple.ItemsSource = CollectionViewSource.GetDefaultView(tokenTrees.First().Tokens);

                dg_elsi_multiple.ItemsSource = null;
                dg_elsi_multiple.Visibility = Visibility.Hidden;
                dg_elsi_multiple.ItemsSource = CollectionViewSource.GetDefaultView(ExtensionMethods.ToDataTable(dicts));
            }
        }
        private void Tb_column_filter_LostFocus(object sender, RoutedEventArgs e) {
            if (columnFilterHistory != null) {
                Regex regColumnFilterH = new Regex(@columnFilterHistory);
                dg_elsi_multiple.Columns.Where(c => !regColumnFilterH.IsMatch(c.Header.ToString())).ToList().ForEach(c => c.Visibility = Visibility.Visible);
            }

            if (@tb_column_filter != null) {
                Regex regColumnFilter = new Regex(@tb_column_filter.Text);
                dg_elsi_multiple.Columns.Where(c => !regColumnFilter.IsMatch(c.Header.ToString())).ToList().ForEach(c => c.Visibility = Visibility.Hidden);
                dg_elsi_multiple.Visibility = Visibility.Visible;
            }

            columnFilterHistory = tb_column_filter.Text;
        }
    }
}
