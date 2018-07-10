using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    abstract public class Intern : Node
    {
        //public const int MAX_TREE_SIZE = 2;
        public override void build_sons()//Sons of an intern node can be an operation/eval_func/const number
        {
            this.num_of_nodes_under = 2;
            /*const int OPERATOR_PROB = 33;
            const int EVAL_FUNC_PROB = 33;
            const int IF_PROB = 33;*/

            for (int i = 0; i <2; i++)// +,-,*,/ are binary operators- always have 2 sons: 2+5, 20/(5*3)...
            {
                int choice = (GetRandom(0, 100)) % 4;//33.333% to be an operation/eval_func/const number

                if (choice == 0)
                    (this.childNodes).Add(new Math_op());

                else if(choice==1)
                    (this.childNodes).Add(new Eval_func());

                else if (choice == 2)
                    (this.childNodes).Add(new Const());

                else
                    (this.childNodes).Add(new If());

            }
                
        }
    }
}
