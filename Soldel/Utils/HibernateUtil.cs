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
using mupeModel;

public class HibernateUtil {

    // ILog mlog = LogManager.GetLogger(GetType());
    private IDictionary<string, ISessionFactory> session_factory_cache = new Dictionary<string, ISessionFactory>();
    private IDictionary<string, string> global_parameters;
    private IList<string> connections;
    private IList<pe_dict> dict_libelles;
    private ISession current_session;

    private static HibernateUtil hibernate_util = null;

    public static HibernateUtil get_instance() {
        if (hibernate_util == null) {
            hibernate_util = new HibernateUtil();
            hibernate_util.load_connections();
            hibernate_util.load_global_parameters();
            hibernate_util.load_global_libelles();
        };
        return hibernate_util;
    }

    public ISession get_current_session() {
        if (current_session == null) {
            throw new Exception("Aucune session n'a été encore ouverte; ouvrir une nouvelle session avec get_session(string)");
        } else {
            return current_session;
        }
    }

    public ISession get_session(String connection_string) {
        ISessionFactory sessionFactory = null;

        if (!session_factory_cache.TryGetValue(connection_string, out sessionFactory)) {
            var hibernateCfg = new NHibernate.Cfg.Configuration().Configure().SetProperty(NHibernate.Cfg.Environment.ConnectionString, connection_string);
            sessionFactory = hibernateCfg.BuildSessionFactory();
            session_factory_cache.Add(connection_string, sessionFactory);
        }
        
        current_session = sessionFactory.OpenSession();
        return current_session;
    }
    private void load_connections() {
        connections = new List<String>() { "Server=localhost;Database=mupe;User Name=root;Password=waldo;SslMode=none" };
 
     }

    private void load_global_parameters() {
        global_parameters = new Dictionary<string, string>();
        global_parameters.Add("USERNAME", System.Environment.GetEnvironmentVariable("USERNAME"));
    }
    private void load_global_libelles() {
        // dict_libelles = get_current_session().CreateCriteria<pe_dict>().List<pe_dict>().ToList();
    }

    public IList<string> get_connections() {
        return connections;
    }

    public string get_user() {
        string user;
        global_parameters.TryGetValue("USERNAME", out user);
        return user;
    }

    public IList<pe_dict> get_dict_libelles() {
        return dict_libelles;
    }
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
