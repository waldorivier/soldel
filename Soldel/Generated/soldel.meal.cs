﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 12.04.2020 16:34:38
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
    /// There are no comments for meal_content, Soldel in the schema.
    /// </summary>
    public partial class meal : i_soldel {

        private IList<meal_content> _l_meal_content = new List<meal_content>();
        private IList<meal_symptom> _l_meal_symptom = new List<meal_symptom>();

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
      
        /// <summary>
        /// There are no comments for meal_content constructor in the schema.
        /// </summary>
        public meal()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for meal_id in the schema.
        /// </summary>
        public virtual long meal_id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for meal_date in the schema.
        /// </summary>
        public virtual System.DateTime meal_date
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for meal_code in the schema.
        /// </summary>
        public virtual long meal_code
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for meal_code_str in the schema.
        /// </summary>
        public virtual string meal_code_str {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for l_meal_content in the schema.
        /// </summary>
        public virtual IList<meal_content> l_meal_content {
            get => this._l_meal_content;
            set => this._l_meal_content = value;
        }

        public virtual IList<meal_symptom> l_meal_symptom {
            get => this._l_meal_symptom;
            set => this._l_meal_symptom = value;
        }

        public override bool Equals(object obj) {
            meal toCompare = obj as meal;
            if (toCompare == null) {
                return false;
            }

            if (!Object.Equals(this.meal_id, toCompare.meal_id))
                return false;

            return true;
        }

        public override int GetHashCode() {
            int hashCode = 13;
            hashCode = (hashCode * 7) + meal_id.GetHashCode();
            return hashCode;
        }
        
        public virtual IList<meal_content> l_meal_content_cat_1 {
            get {
                return l_meal_content_cat(1);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat_2 {
            get {
                return l_meal_content_cat(2);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat_3 {
            get {
                return l_meal_content_cat(3);
            }
        }
        
        public virtual IList<meal_content> l_meal_content_cat_4 {
            get {
                return l_meal_content_cat(4);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat_5 {
            get {
                return l_meal_content_cat(5);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat_6 {
            get {
                return l_meal_content_cat(6);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat_7 {
            get {
                return l_meal_content_cat(7);
            }
        }

        public virtual IList<meal_content> l_meal_content_cat(int cat_id) {
            return l_meal_content.Where(x => x.food.caterory.category_id == cat_id).ToList<meal_content>();
        }

        public virtual IList< object> l_meal_code_str {
            get {
                return new[] { new { name = "matin", code = (long)1},
                               new { name = "midi",  code = (long)2},
                               new { name = "soir",  code = (long)3}
                             };
                }
        }

        public virtual IList<food> l_food_cat_1 {
            get {
                return hibernate_util.get_instance().get_l_food(1);
            }
        }

        public virtual IList<food> l_food_cat_2 {
            get {
                return hibernate_util.get_instance().get_l_food(2);
            }
        }

        public virtual IList<food> l_food_cat_3 {
            get {
                return hibernate_util.get_instance().get_l_food(3);
            }
        }

        public virtual IList<food> l_food_cat_4 {
            get {
                return hibernate_util.get_instance().get_l_food(4);
            }
        }

        public virtual IList<food> l_food_cat_5 {
            get {
                return hibernate_util.get_instance().get_l_food(5);
            }
        }

        public virtual IList<food> l_food_cat_6{
            get {
                return hibernate_util.get_instance().get_l_food(6);
            }
        }

        public virtual IList<food> l_food_cat_7 {
            get {
                return hibernate_util.get_instance().get_l_food(7);
            }
        }

        public virtual IList<symptom> l_symptom {
            get {
                return hibernate_util.get_instance().get_l_symptom();
            }
        }
        
        public virtual IList<meal_symptom> l_meal_symptom_ {
            get {
                return l_meal_symptom.Where(x => x.symptom_id > 0).ToList<meal_symptom>();
            }
        }

        #region I_SOLDEL

        public virtual void add_child(object child) {
            if (child is meal_content) {
                meal_content mc = (meal_content)child;
                this.l_meal_content.Add(mc);
                mc.meal = this;
                mc.meal_id = this.meal_id;
            }

            if (child is meal_symptom) {
                meal_symptom ms = (meal_symptom)child;
                this.l_meal_symptom.Add(ms);
                ms.meal = this;
                ms.meal_id = this.meal_id;
            }
        }

        public virtual bool can_add_child(object child) {
            bool can_add = false;
            if (child is meal_content) {
                meal_content mc = (meal_content)child;
                int cnt = l_meal_content.Where(x => x.food.Equals(mc.food)).Count();
                if (cnt==0) {
                    can_add = true;
                }
            }

            if (child is meal_symptom) {
                meal_symptom ms = (meal_symptom)child;
                int cnt = l_meal_symptom.Where(x => x.symptom.Equals(ms.symptom)).Count();
                if (cnt == 0) {
                    can_add = true;
                }
            }
          
            return can_add;
        }

        public virtual bool can_remove_me() { 
            return true;
        }

        public virtual void remove_me() {
        }

        public virtual bool is_persistant() {
            return true;
        }

        public virtual bool can_update() {
            return l_meal_content.Any(x => x.can_update() == true) ||
                   l_meal_symptom.Any(x => x.can_update() == true);
        }

        public virtual void update() {
            update_l_meal_content();
            update_l_meal_symptom();
        }

        private void update_l_meal_content() {
            IList<i_soldel> l_to_add = new List<i_soldel>();
            IList<i_soldel> l_to_remove = new List<i_soldel>();

            foreach (meal_content mc in l_meal_content) {
                if (mc.can_update()) {
                    meal_content n_mc = new meal_content(this, mc._food);
                    if (mc._food.ToString().Equals("vide")) {
                        l_to_remove.Add(mc);
                    } else if (can_add_child(n_mc)) {
                        l_to_add.Add(n_mc);
                        l_to_remove.Add(mc);
                    }
                }
            }

            foreach (i_soldel mc in l_to_remove) {
                mc.remove_me();
            }

            foreach (i_soldel mc in l_to_add) {
                add_child(mc);
            }
        }

        private void update_l_meal_symptom() {
            IList<i_soldel> l_to_add = new List<i_soldel>();
            IList<i_soldel> l_to_remove = new List<i_soldel>();

            foreach (meal_symptom mc in l_meal_symptom) {
                if (mc.can_update()) {
                    meal_symptom n_mc = new meal_symptom(this, mc._symptom);
                    if (mc._symptom.ToString().Equals("vide")) {
                        l_to_remove.Add(mc);
                    } else if (can_add_child(n_mc)) {
                        l_to_add.Add(n_mc);
                        l_to_remove.Add(mc);
                    }
                }
            }

            foreach (i_soldel mc in l_to_remove) {
                mc.remove_me();
            }

            foreach (i_soldel mc in l_to_add) {
                add_child(mc);
            }
        }

        public virtual i_soldel copy() {
            var copy = new meal();
            copy_object.copy<meal>(this, copy);

            copy.meal_id = hibernate_util.get_instance().generate_meal_id();
            copy.meal_date = DateTime.Today;

            foreach (meal_content mc in l_meal_content) {
                meal_content mc_copy = (meal_content)mc.copy();

                mc_copy.food_id = mc.food_id;
                mc_copy.food = mc.food;
                mc_copy._food = mc_copy.food;

                copy.add_child(mc_copy);
            }

            foreach (meal_symptom ms in l_meal_symptom) {
                meal_symptom ms_copy = (meal_symptom)ms.copy();

                ms_copy.symptom_id = ms.symptom_id;
                ms_copy.symptom = ms.symptom;
                ms_copy._symptom = ms.symptom;

                copy.add_child(ms_copy);
            }
            return copy;
        }

        #endregion
    }
}
