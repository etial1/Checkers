using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    abstract public class Leaf:Node
    {
        public Leaf()
        {
            //father
            this.childNodes = null;
        }
        public override void build_sons()
        {
            this.num_of_nodes_under = 0;
        }

      
    }
}
