using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class Const:Leaf
    {
        public Const()
        {
            
            this.value = GetRandom(0, 100);
            this.sign = (this.value).ToString();
            this.num_of_nodes_under = 0;
        }

        public Const(Const old)
        {

            this.value = old.value;
            this.sign =old.sign;
            this.num_of_nodes_under = old.num_of_nodes_under;
        }

        public override void eval()
        {
           
        }
    }
}
