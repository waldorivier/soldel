﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 19.04.2020 21:03:51
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
    /// There are no comments for caterory, Soldel in the schema.
    /// </summary>
    public partial class caterory {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for caterory constructor in the schema.
        /// </summary>
        public caterory()
        {
            this.foods = new List<food>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for category_id in the schema.
        /// </summary>
        public virtual long category_id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for name in the schema.
        /// </summary>
        public virtual string name
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for foods in the schema.
        /// </summary>
        public virtual IList<food> foods
        {
            get;
            set;
        }
    }

}
