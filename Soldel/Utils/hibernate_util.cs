using NHibernate;
using NHibernate.Cfg;
using log4net;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Dynamic;
using mupeModel;

namespace mupeModel.Utils {
    public class hibernate_util {

        // ILog mlog = LogManager.GetLogger(GetType());
        private IDictionary<string,ISessionFactory> session_factory_cache = new Dictionary<string,ISessionFactory>();
        private IDictionary<string,string> global_parameters;
        private IList<string> connections;
        private IList<pe_libl> dict_libelles;
        private ISession current_session;

        private static hibernate_util hibernate_util_ = null;

        public static hibernate_util get_instance() {
            if(hibernate_util_ == null) {
                hibernate_util_ = new hibernate_util();
                hibernate_util_.load_connections();
                hibernate_util_.load_global_parameters();
            };
            return hibernate_util_;
        }

        public ISession get_current_session() {
            if(current_session == null) {
                throw new Exception("Aucune session n'a été encore ouverte; ouvrir une nouvelle session avec get_session(string)");
            } else {
                return current_session;
            }
        }

        public ISession get_session(String connection_string) {
            ISessionFactory factory = null;
            if(!this.session_factory_cache.TryGetValue(connection_string,out factory)) {
                Configuration configuration = new Configuration().Configure().SetProperty("connection.connection_string",connection_string);
                configuration.SetInterceptor(new audit_interceptor());
                factory = configuration.BuildSessionFactory();
                this.session_factory_cache.Add(connection_string,factory);
            }
            this.current_session = factory.OpenSession();
            this.load_global_libelles();
            return this.current_session;
        }

        private void load_connections() {
            //connections = new List<String>() { "Server=localhost;Database=mupe;User Name=root;Password=waldo;SslMode=none" };
            connections = new List<String>() {"Data Source = LABCIT; User ID = PADEV96_DATA; Password = PADEV96_DATA",
                                        "Data Source=OUTSTST; User Id = tst_2; Password = tst_2",
                                        "Data Source=LABCITST; User Id = pedev_tst_a1; Password = pedev_tst_a1",
                                        "Data Source = LABCITST; User ID = PEDEV_TST_A1; Password = PEDEV_TST_A1",

                                        "Data Source=QALIC; User ID = qa_cpne; Password = qa_cpne",
                                        "Data Source=QALIC; User ID = qa_cpne_2019; Password = qa_cpne_2019",
                                        "Data Source=QCASP1; User Id = qc_bcn; Password = qc_bcn",
                                        "Data Source=QCASP1; User Id = qc_cap; Password = qc_cap",
                                        "Data Source=OUTCATST; User Id = qc_lausanne; Password = qc_lausanne",
                                        
                                        "Data Source=ASP1;User Id=prod_cap;Password=prod_cap",
                                        "Data Source=outcasp;User Id=prod_lausanne;Password=prod_lausanne",
                                        "Data Source=ASP1; User Id = prod_vsao2; Password = prod_vsao2",

                                        "Data Source=ASP2;User Id=prod_cpju;Password=prod_cpju",
                                        "Data Source=OUTCASP; User Id = prod_tetra; Password=prod_tetra",

                                        "Data Source=outsrc; User Id = prod_1; Password=prod_1",
                                        "Data Source=OUTSRC; User Id = prod_2; Password = prod_2",
                                        "Data Source=OUTSRC; User Id = prod_3; Password = prod_3",
                                        "Data Source=OUTSRC; User Id = prod_5; Password = prod_5",

                                        "Data Source=OUTSRC; User Id = prod_bcf; Password=prod_bcf",
                                        "Data Source=OUTSRC; User Id = prod_bien; Password=prod_bien",
                                        "Data Source=OUTSRC; User Id = prod_cicr; Password=prod_cicr",
                                        "Data Source=OUTSRC; User Id = prod_chop; Password=prod_chop",
                                        "Data Source=OUTSRC; User Id = prod_gass; Password=prod_gass",
                                        "Data Source=OUTSRC; User Id = prod_dentsply; Password=prod_dentsply",
                                        "Data Source=OUTSRC; User Id = prod_martigny; Password=prod_martigny",
                                        "Data Source=OUTSRC; User Id = prod_mikr; Password=prod_mikr",
                                        "Data Source=OUTSRC; User Id = prod_mps; Password=prod_mps",
                                        "Data Source=OUTSRC; User Id = prod_naef; Password=prod_naef",
                                        "Data Source=OUTSRC; User Id = prod_optio1e; Password=prod_optio1e",
                                        "Data Source=OUTSRC; User Id = prod_pictet; Password=prod_pictet",
                                        "Data Source=OUTSRC; User Id = prod_thomreut; Password=prod_thomreut",
                                        "Data Source=SWSTAFF;User Id=prod_usse;Password=prod_usse",
                                        };
        }

        private void load_global_parameters() {
            global_parameters = new Dictionary<string,string>();
            global_parameters.Add("USERNAME",System.Environment.GetEnvironmentVariable("USERNAME"));
        }

        private void load_global_libelles() {
            dict_libelles = get_current_session().CreateCriteria<pe_libl>().List<pe_libl>().ToList();
        }

        public IList<string> get_connections() {
            return connections;
        }

        public string get_user() {
            string user;
            global_parameters.TryGetValue("USERNAME",out user);
            return user;
        }

        public IList<pe_libl> get_dict_libelles() {
            return dict_libelles;
        }

        public string generate_muta_id() {
            return get_current_session().CreateSQLQuery("SELECT MAX (to_number(pe_muta_id)) + 1 from pe_muta").UniqueResult().ToString();
        }

        public string generate_grmu_id() {
            return get_current_session().CreateSQLQuery("SELECT MAX (to_number(pe_grmu_id)) + 1 from pe_grmu").UniqueResult().ToString();
        }

        public string generate_attr_id() {
            return get_current_session().CreateSQLQuery("SELECT MAX (to_number(pe_attr_id)) + 1 from pe_attr").UniqueResult().ToString();
        }
    }

    public static class extension_method {
        public static DataTable to_data_table<T>(this IList<T> data) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach(PropertyDescriptor prop in properties)
                // table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                table.Columns.Add(prop.Name);
            foreach(T item in data) {
                DataRow row = table.NewRow();
                foreach(PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);

            }
            return table;
        }

        public static DataTable to_data_table(List<IDictionary<string,string>> list) {
            DataTable table = new DataTable();
            if(list.Count == 0)
                return table;

            var columns_to_select = list.SelectMany(dict => dict.Keys).Distinct().ToList();

            IList<string> persitent_columns_headers = new List<string>() { "DTMUTX","CODMOU","XDTMUT","DTNAIS" };
            columns_to_select.RemoveAll(header => persitent_columns_headers.Contains(header));
            columns_to_select.InsertRange(0,persitent_columns_headers);

            try {
                table.Columns.AddRange(columns_to_select.Select(c => new DataColumn(c)).ToArray());
                foreach(Dictionary<string,string> item in list) {
                    var row = table.NewRow();
                    foreach(var key in item.Keys.Intersect(columns_to_select)) {
                        row[table.Columns.IndexOf(key.ToString())] = item[key];
                    }
                    table.Rows.Add(row);
                }
            } catch {
                // message d'erreur en principe si la règle d'unicité des colonnes de la table est violée
                // Ajouter le message à une listbox de messages....moins intrusif...
            }
            return table;
        }
    }

    public static class copy_object {

        public static void copy<T>(T copy_from,T copy_to) {
            if(copy_from == null || copy_to == null)
                throw new Exception("Must not specify null parameters");

            // ce sont les types de base des propriétés que l'on veut copier
            string[] a_types_names = { "System.String","System.Boolean","System.DateTime","System.Decimal","System.Int32" };

            // ce sont les propriétés que l'on veut exlure de la copie 
            IList<string> properties_to_exclude = new List<string>() { "user_cre","user_maj","dh_cre","dh_maj" };

            var properties = copy_from.GetType().GetProperties();
            foreach(var property in properties) {

                if(properties_to_exclude.Contains(property.Name)) {
                    continue;
                }

                try {
                    string str = null;

                    // pour les nullables (en particulier), il faut extraire le type sous-jacent : NULLABLE<Int32> => Int32

                    Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    if(underlyingType != null) {
                        str = underlyingType.ToString();
                    } else {
                        str = property.PropertyType.ToString();
                    }

                    if(a_types_names.Contains<string>(str)) {
                        object obj2 = property.GetValue(copy_from);
                        property.SetValue(copy_to,obj2);
                    }
                } catch(Exception) {
                }
            }
        }
    }
}
