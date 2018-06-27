﻿using NHibernate;
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
public class HibernateUtil {
    // ILog mlog = LogManager.GetLogger(GetType());
    private IDictionary<string, ISessionFactory> sessionFactoryCache = new Dictionary<string, ISessionFactory>();
    private static HibernateUtil m_hibernate_util = null;

    public static HibernateUtil get_instance() {
        if (m_hibernate_util == null) {
            m_hibernate_util = new HibernateUtil();
        };
        return m_hibernate_util;
    }

    public ISession get_session(String connection_string) {
        ISessionFactory sessionFactory = null;

        if (!sessionFactoryCache.TryGetValue(connection_string, out sessionFactory)) {
            var hibernateCfg = new NHibernate.Cfg.Configuration().Configure().SetProperty(NHibernate.Cfg.Environment.ConnectionString, connection_string);
            sessionFactory = hibernateCfg.BuildSessionFactory();
            sessionFactoryCache.Add(connection_string, sessionFactory);
        }
        return sessionFactory.OpenSession();
    }

    public List<string> get_connections =
                // new List<String>() { "Server=localhost;Database=mupe;User Name=root;Password=waldo;" };
                new List<String>() {"Data Source = LABCIT; User ID = PADEV96_DATA; Password = PADEV96_DATA",
                                    "Data Source = QALIC; User ID = qa_cpne; Password = qa_cpne",
                                    "Data Source = QALIC; User ID = qa_cpne_2019; Password = qa_cpne_2019",
                                    "Data Source = LABCITST; User ID = PEDEV_TST_A1; Password = PEDEV_TST_A1",
                                    "Data Source=QCASP1; User Id = qc_bcn; Password = qc_bcn",
                                    "Data Source=QCASP1; User Id = qc_cap; Password = qc_cap",
                                    "Data Source=ASP1;User Id=prod_cap;Password=prod_cap",
                                    "Data Source=outcatst;User Id=qc_laus_2018;Password=qc_laus_2018",
                                    "Data Source=OUTCATST; User Id = qc_lausanne; Password = qc_lausanne",
                                    "Data Source=outcasp;User Id=prod_lausanne;Password=prod_lausanne",
                                    "Data Source=OUTSRC; User Id = prod_2; Password = prod_2",
                                    "Data Source=OUTSRC; User Id = prod_3; Password = prod_3",
                                    "Data Source=OUTSTST; User Id = tst_2; Password = tst_2",
                                    "Data Source=LABCITST; User Id = pedev_tst_a1; Password = pedev_tst_a1",
                                    "Data Source=OUTSRC; User Id = prod_optio1e; Password=prod_optio1e",
                                    "Data Source=outsrc; User Id = prod_1; Password=prod_1"};
}


public static class ExtensionMethods {
    public static DataTable to_data_table<T>(this IList<T> data) {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            // table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            table.Columns.Add(prop.Name);
        foreach (T item in data) {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

            table.Rows.Add(row);

        }
        return table;
    }

    public static DataTable to_data_table(List<IDictionary<string, string>> list) {
        DataTable table = new DataTable();
        if (list.Count == 0)
            return table;

        var columns_to_select = list.SelectMany(dict => dict.Keys).Distinct().ToList();

        IList<string> persitent_columns_headers = new List<string>() { "DTMUTX", "CODMOU", "XDTMUT", "DTNAIS" };
        columns_to_select.RemoveAll(header => persitent_columns_headers.Contains(header));
        columns_to_select.InsertRange(0, persitent_columns_headers);

        try {
            table.Columns.AddRange(columns_to_select.Select(c => new DataColumn(c)).ToArray());
            foreach (Dictionary<string, string> item in list) {
                var row = table.NewRow();
                foreach (var key in item.Keys.Intersect(columns_to_select)) {
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

public static class CopyObject {
    public static void copy<T>(T copy_from, T copy_to) {
        if (copy_from == null || copy_to == null)
            throw new Exception("Must not specify null parameters");

        // ne copier que les propriétés des types ci-dessous
        string[] a_type_names = { "System.String", "System.Boolean", "System.DateTime", "System.Decimal", "System.Int32" };

        var properties = copy_from.GetType().GetProperties();
        foreach (var p in properties.Where(prop => prop.CanRead &&
                                                   prop.CanWrite &&
                                                   a_type_names.Contains(prop.PropertyType.FullName))) {

            object copyValue = p.GetValue(copy_from);
            p.SetValue(copy_to, copyValue);
        }
    }
}
