using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mupeModel {
    public class soldel {

        public virtual IList<String> true_false_list {
            get {
                return new List<String>() { " ","F","T" };
            }
        }

        #region TREEVIEW

        public virtual bool is_expanded => false;

        public virtual string tree_view_item_foreground => "black";

        #endregion

    }
}
