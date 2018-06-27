using System;
using System.Collections.Generic;
using System.Data;
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
using mupeModel;
using System.Collections;
using System.ComponentModel;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Soldel.Views {
    /// <summary>
    /// Logique d'interaction pour uc_elca.xaml
    /// </summary>
    public partial class    uc_elca : UserControl {
        private DataTable _dt;
        public DataTable _dt_filtered { get; set; }

        private ICollectionView source;
        public ISession session { get; set; }

        public uc_elca() {
            InitializeComponent();
            
            uc_select_connection.AddHandler(ListBox.SelectionChangedEvent, new RoutedEventHandler(Cb_ip_list_SelectionChanged));

            cb_cate.DropDownOpened += Cb_cate_DropDownOpened;
            cb_cate.SelectionChanged += Cb_cate_SelectionChanged;

            cb_ddv.DropDownOpened += Cb_ddv_DropDownOpened;
            cb_ddv.SelectionChanged += Cb_ddv_SelectionChanged;

            cb_ip.DropDownOpened += Cb_ip_DropDownOpened;
            cb_ip.SelectionChanged += Cb_ip_SelectionChanged;

            cb_cas.DropDownOpened += Cb_cas_DropDownOpened;
            cb_cas.SelectionChanged += Cb_cas_SelectionChanged;

            cb_plan.DropDownOpened += Cb_plan_DropDownOpened;
            cb_plan.SelectionChanged += Cb_plan_SelectionChanged;

            btn_filter.Click += Btn_filter_Click;
        }
        
        private void Cb_ip_list_SelectionChanged(object sender, RoutedEventArgs e) {
            var noIp = uc_select_connection.cb_ip_list.SelectedValue;
            if (noIp != null)
            {
                List<pe_elca> list = uc_select_connection.session.CreateCriteria<pe_elca>().Add(Restrictions.Eq("no_ip", noIp)).List<pe_elca>().ToList();
                DataTable dt = ExtensionMethods.to_data_table(list);
                _dt = _dt_filtered = dt;

                source = CollectionViewSource.GetDefaultView(list);
                dg_elca.ItemsSource = null;
                dg_elca.ItemsSource = source;
            }
        }
                
        private void Cb_ddv_SelectionChanged(object sender,
                 SelectionChangedEventArgs e) {
            view_filter((ComboBox)sender, "PE_CHAI_DDV");
        }

        private void Cb_ddv_DropDownOpened(object sender, EventArgs e) {
            cb_item_fill((ComboBox)sender, "PE_CHAI_DDV");
        }

        private void Cb_ip_SelectionChanged(object sender,
                SelectionChangedEventArgs e) {
            view_filter((ComboBox)sender, "NO_IP");
        }

        private void Cb_ip_DropDownOpened(object sender, EventArgs e) {
            cb_item_fill((ComboBox)sender, "NO_IP");
        }

        private void Cb_cas_SelectionChanged(object sender,
                SelectionChangedEventArgs e) {
            view_filter((ComboBox)sender, "NO_CAS");
        }

        private void Cb_cas_DropDownOpened(object sender, EventArgs e) {
            cb_item_fill((ComboBox)sender, "NO_CAS");
        }

        private void Cb_cate_SelectionChanged(object sender,
             SelectionChangedEventArgs e) {
            view_filter((ComboBox)sender, "NO_CATE");
        }

        private void Cb_cate_DropDownOpened(object sender, EventArgs e) {
            cb_item_fill((ComboBox)sender, "NO_CATE");

        }

        private void Cb_plan_SelectionChanged(object sender,
                SelectionChangedEventArgs e) {
            view_filter((ComboBox)sender, "NO_PLAN");
        }

        private void Cb_plan_DropDownOpened(object sender, EventArgs e) {
            cb_item_fill((ComboBox)sender, "NO_PLAN");
        }

        private void Btn_filter_Click(object sender, RoutedEventArgs e) {
            cb_cas.ItemsSource = null;
            cb_ip.ItemsSource = null;
            cb_ddv.ItemsSource = null;
            cb_cate.ItemsSource = null;
            cb_plan.ItemsSource = null;

            _dt_filtered = _dt;
        }

        private void view_filter(ComboBox cb, String field) {
            String sel_value = (String)cb.SelectedValue;

            try {
                DateTime d = DateTime.Parse(sel_value);
                sel_value = d.ToString("dd.MM.yyyy 00:00:00");
            } catch (Exception) {
            }

            /*       La mise en place d'une vue n'améliore pas la vélocité du refresh, c'est win 7 qui n'est pas adapté à wpf
                          source.Filter = item  => {
                            pe_elca e = item as pe_elca;

                            if (e == null) return false;

                            return e.no_cas.Equals(Convert.ToInt32(sel_value)) ;
                        };
            */
            var x = from DataRow dr in _dt_filtered.AsEnumerable()
                    where dr.Field<String>(field) == sel_value
                    select dr;

            if (x.Count() > 0) {
                _dt_filtered = x.CopyToDataTable();
                dg_elca.ItemsSource = _dt_filtered.AsDataView();
            }
        }

        private void cb_item_fill(ComboBox cb, String field) {
            var x = (from DataRow dr in _dt_filtered.AsEnumerable()
                     select dr.Field<string>(field)).Distinct();
            try {
                DateTime d = DateTime.Parse(x.First());
                x = x.Select(s => DateTime.Parse(s)).ToList().OrderByDescending(s => s).Select(s => s.ToShortDateString());
            } catch (Exception) {
            }

            cb.ItemsSource = x;
        }

        private void cb_item_fill_from_db(ComboBox cb, String field) {
            var x = (from DataRow dr in _dt_filtered.AsEnumerable()
                     select dr.Field<string>(field)).Distinct();

            try {
                DateTime d = DateTime.Parse(x.First());
                x = x.Select(s => DateTime.Parse(s)).ToList().OrderByDescending(s => s).Select(s => s.ToShortDateString());
            } catch (Exception) {
            }

            cb.ItemsSource = x;
        }
    }

    public class elcaProjection {
        public string nom_elem { get; set; }
        public string nom_logi { get; set; }

        public elcaProjection(string elem, string logi) {
            nom_elem = elem;
            nom_logi = logi;
        }
    }
}
