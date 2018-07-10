using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class cond_node: Intern
    {
        List<string> cond_ops = new List<string> { "=", ">", "<", ">=", "<=", "!=" };

        public cond_node()
        {
            this.sign = cond_ops[(GetRandom(0, 100) % cond_ops.Count())];//Random boolean operator from the operators list- cond_ops
            this.value = -1;//No value yet
            this.childNodes = new List<Node>();
            this.num_of_nodes_under = 2;

        }
        public cond_node(cond_node old)
        {
            this.sign = old.sign;//Random boolean operator from the operators list- cond_ops
            this.value = old.value;//No value yet
            this.childNodes = new List<Node>();
            this.num_of_nodes_under = old.num_of_nodes_under;

        }

        public override void eval()
        {
            this.num_of_nodes_under = 2;
            switch (this.sign)
            {
                
                case "=":
                    this.value = childNodes[0].value == childNodes[1].value ? 1 : 0;
                    break;

                case ">":
                    this.value = childNodes[0].value > childNodes[1].value ? 1 : 0;
                    break;

                case "<":
                    this.value = childNodes[0].value < childNodes[1].value ? 1 : 0;
                    break;

                case ">=":
                    this.value = childNodes[0].value >= childNodes[1].value ? 1 : 0;
                    break;

                case "<=":
                    this.value = childNodes[0].value <= childNodes[1].value ? 1 : 0;

                    break;
                case "!=":
                    this.value = childNodes[0].value != childNodes[1].value ? 1 : 0;
                    break;

                default:
                    Console.WriteLine("Error in cond_ops in func eval in the switch case- only boolean operator are allowed -error. Bye Bye!");
                    Environment.Exit(1);
                    break;
            }
        }

        public override void build_sons()//Sons of an intern node can be an operation/eval_func/const number
        {
            //SIZE = SIZE == 1 ? 2 : SIZE;
            /*const int OPERATOR_PROB = 33;
            const int EVAL_FUNC_PROB = 33;
            const int IF_PROB = 33;*/
            this.num_of_nodes_under = 2;
            for (int i = 0; i < 2; i++)// +,-,*,/ are binary operators- always have 2 sons: 2+5, 20/(5*3)...
            {
                
                    (this.childNodes).Add(new Const());


            }

        }
    }
}
