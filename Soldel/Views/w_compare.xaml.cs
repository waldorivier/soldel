using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using mupeModel.Utils;
using NHibernate.Mapping;

namespace Soldel.Views {
    public partial class w_compare : Window {
        Stack<Token> StackToken = new Stack<Token>();

        public w_compare() {
            InitializeComponent();

            btn_compare.Click += Btn_compare_Click;
        }

        private void Btn_compare_Click(object sender, RoutedEventArgs e) {
            DataTable d1 = uc_elca_1._dt_filtered;
            DataTable d2 = uc_elca_2._dt_filtered;

            if (d1 != null & d2 != null) {

                try {
                    var intersection = d1.AsEnumerable().Intersect(d2.AsEnumerable(), new elcaComparer());
                    var intersection_proj = from dr in intersection select new elcaProjection(dr.Field<String>("nom_elem"), 
                                                                                              dr.Field<String>("nom_logi"), 
                                                                                              dr.Field<String>("no_cas"),
                                                                                              dr.Field<String>("pe_chai_ddv"));

                    var difference_1 = d1.AsEnumerable().Except(d2.AsEnumerable(), new elcaComparer());
                    var difference_1_proj = from dr in difference_1 select new elcaProjection(dr.Field<String>("nom_elem"),
                                                                                              dr.Field<String>("nom_logi"),
                                                                                              dr.Field<String>("no_cas"),
                                                                                              dr.Field<String>("pe_chai_ddv"));

                    var difference_2 = d2.AsEnumerable().Except(d1.AsEnumerable(), new elcaComparer());
                    var difference_2_proj = from dr in difference_2 select new elcaProjection(dr.Field<String>("nom_elem"),
                                                                                              dr.Field<String>("nom_logi"),
                                                                                              dr.Field<String>("no_cas"),
                                                                                              dr.Field<String>("pe_chai_ddv"));

                    w_compare_result result = new w_compare_result();

                    result.g_intersect.ItemsSource = CollectionViewSource.GetDefaultView(intersection_proj);
                    result.g_difference_1.ItemsSource = CollectionViewSource.GetDefaultView(difference_1_proj.Count() > 0 ? difference_1_proj : null);
                    result.g_difference_2.ItemsSource = CollectionViewSource.GetDefaultView(difference_2_proj.Count() > 0 ? difference_2_proj : null);

                    result.ShowDialog();
                }catch (Exception ex) {
                    var message = ex.Message;
                }
            }
        }

        protected class elcaComparer : IEqualityComparer<DataRow> {
            public bool Equals(DataRow x, DataRow y) {
                if (Object.ReferenceEquals(x, y)) return true;

                if (x != null && y != null) {
                    if (x["nom_logi"] == DBNull.Value || y["nom_logi"] == DBNull.Value) {
                        return x.Field<String>("nom_elem").Equals(y.Field<String>("nom_elem"));
                    } else {
                        return x.Field<String>("nom_elem").Equals(y.Field<String>("nom_elem")) &&
                               x.Field<String>("nom_logi").Equals(y.Field<String>("nom_logi")) &&
                               x.Field<String>("no_cas").Equals(y.Field<String>("no_cas")) &&
                               x.Field<String>("pe_chai_ddv").Equals(y.Field<String>("pe_chai_ddv"));
                    }
                } else {
                    return false;
                }
            }

            public int GetHashCode(DataRow x) {
                if (x != null) {

                    if (x["nom_logi"] == DBNull.Value) {
                        return x.Field<String>("nom_elem").GetHashCode();
                    } else {
                        return x.Field<String>("nom_elem").GetHashCode() ^ x.Field<String>("nom_logi").GetHashCode();
                    }
                } else {
                    return 0;
                }
            }
        }
    }
}