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
using mupeModel.Utils;

namespace mupeModel
{

    /// <summary>
    /// There are no comments for food, Soldel in the schema.
    /// </summary>
    public partial class food : i_soldel {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        
        #endregion
        /// <summary>
        /// There are no comments for food constructor in the schema.
        /// </summary>
        public food()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for food_id in the schema.
        /// </summary>
        public virtual long food_id
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
        /// There are no comments for caterory in the schema.
        /// </summary>
        public virtual caterory caterory
        {
            get;
            set;
        }

        #region I_SOLDEL

        public virtual i_soldel shallow_copy() {
            var copy = new food();
            copy_object.copy<food>(this, copy);

            return copy;
        }

        public void add_child(object child) {
            throw new NotImplementedException();
        }

        public bool can_add_child(object child) {
            throw new NotImplementedException();
        }

        public bool can_remove_me() {
            throw new NotImplementedException();
        }

        public void remove_me() {
            throw new NotImplementedException();
        }

        public bool is_persistant() {
            throw new NotImplementedException();
        }

        #endregion  
    }

}