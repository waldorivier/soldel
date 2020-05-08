using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mupeModel.Utils {
    public class food_post_load_listener : IPostLoadEventListener {
        public virtual void OnPostLoad(PostLoadEvent @event) {
            if (@event.Entity is mupeModel.meal_content) {
                meal_content mc = (meal_content)@event.Entity;
                mc._food = mc.food;
            }
        }
     }
}
