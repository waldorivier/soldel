﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 21.01.2018 16:46:29
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace mupeModel
{

    /// <summary>
    /// There are no comments for pe_elca, Soldel in the schema.
    /// </summary>
    public partial class pe_elca {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for pe_elca constructor in the schema.
        /// </summary>
        public pe_elca()
        {
            this.pe_elca_id = @"";
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for pe_elca_id in the schema.
        /// </summary>
        public virtual string pe_elca_id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_ip in the schema.
        /// </summary>
        public virtual int no_ip
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_plan in the schema.
        /// </summary>
        public virtual int no_plan
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_cas in the schema.
        /// </summary>
        public virtual int no_cas
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_cate in the schema.
        /// </summary>
        public virtual int no_cate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for tymouv in the schema.
        /// </summary>
        public virtual string tymouv
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for pe_chai_ddv in the schema.
        /// </summary>
        public virtual System.DateTime pe_chai_ddv
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_bloc in the schema.
        /// </summary>
        public virtual decimal no_bloc
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for u_version in the schema.
        /// </summary>
        public virtual string u_version
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for nom_elem in the schema.
        /// </summary>
        public virtual string nom_elem
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for nom_logi in the schema.
        /// </summary>
        public virtual string nom_logi
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for no_volet in the schema.
        /// </summary>
        public virtual System.Nullable<decimal> no_volet
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for pos_volet in the schema.
        /// </summary>
        public virtual System.Nullable<decimal> pos_volet
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for codaff_elvf in the schema.
        /// </summary>
        public virtual string codaff_elvf
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for user_maj in the schema.
        /// </summary>
        public virtual string user_maj
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for dh_maj in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> dh_maj
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for user_cre in the schema.
        /// </summary>
        public virtual string user_cre
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for dh_cre in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> dh_cre
        {
            get;
            set;
        }
    }

}
