﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 24.04.2020 21:53:34
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

namespace mupeModel {

    /// <summary>
    /// There are no comments for meal_content, Soldel in the schema.
    /// </summary>
    public partial class meal_content : i_soldel {

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();


        public meal_content(meal meal, food food) {
            this.meal = meal;
            this.food = food;

            this.meal_id = meal.meal_id;
            this.food_id = food.food_id;

            this._food = food;

            OnCreated();
        }

        public override bool Equals(object obj) {
            meal_content toCompare = obj as meal_content;
            if (toCompare == null) {
                return false;
            }

            if (!Object.Equals(this.food_id, toCompare.food_id))
                return false;
            if (!Object.Equals(this.meal_id, toCompare.meal_id))
                return false;

            return true;
        }

        public override int GetHashCode() {
            int hashCode = 13;
            hashCode = (hashCode * 7) + food_id.GetHashCode();
            hashCode = (hashCode * 7) + meal_id.GetHashCode();
            return hashCode;
        }


        /// <summary>
        /// There are no comments for meal_content constructor in the schema.
        /// </summary>
        public meal_content() {
            OnCreated();
        }


        /// <summary>
        /// There are no comments for food_id in the schema.
        /// </summary>
        public virtual long food_id {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for meal_id in the schema.
        /// </summary>
        public virtual long meal_id {
            get;
            set;
        }
        
        public virtual food food {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for meal in the schema.
        /// </summary>
        public virtual meal meal {
            get;
            set;
        }

        public virtual food _food {
            get;
            set;
        }

        #region I_SOLDEL

        public virtual void add_child(object child) {
            throw new NotImplementedException();
        }

        public virtual bool can_add_child(object child) {
            throw new NotImplementedException();
        }

        public virtual bool can_remove_me() {
            return true;
        }

        public virtual void remove_me() {
            food.l_meal_content.Remove(this);
            meal.l_meal_content.Remove(this);
         }

        public virtual bool is_persistant() {
            throw new NotImplementedException();
        }

        public virtual i_soldel shallow_copy() {
            throw new NotImplementedException();
        }

        public virtual bool is_modified() {
            return _food != null & _food != food;
        }

        #endregion
    }
}
