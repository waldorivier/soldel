using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mupeModel.Utils {
    class persister {
        internal static void add_child(Delegate a, object child) {

            a.DynamicInvoke(child); 

        }
    }
}
