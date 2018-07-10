using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class Math_op : Intern
    {
        
        const int OPERATOR_PROB = 70;//70% the son will be an operator
        const int EVAL_FUNC_PROB = 50;//70% the son will be an operator
        //const int MAX_NUM_OF_CHILDREN = 2; 
        List<string> math_ops = new List<string> { "+", "-", "*", "/", "^", "sqrt" };
        public Math_op()
        {
            this.sign = math_ops[(GetRandom(0, 100) % math_ops.Count())];//Random operator from the operators list- math_ops
            this.value = 0;//No value yetroot.childNodes = new List<Node>();
            this.childNodes = new List<Node>();
            this.num_of_nodes_under = 3;
            
        }
        public Math_op(Math_op old)
        {
            this.sign = old.sign;
            this.value = old.value;
            this.childNodes = new List<Node>();
            this.num_of_nodes_under = old.num_of_nodes_under;

        }
        public override void eval()
        {
            this.num_of_nodes_under = 2;
            switch (this.sign)
            {
                //+,-,*,/ acts the same and ^,sqrt acts the same
                case "+":
                    this.value = childNodes[0].value + childNodes[1].value;

                    break;
                case "-":
                    this.value = childNodes[0].value - childNodes[1].value;
                    break;
                case "*":
                    this.value = childNodes[0].value * childNodes[1].value;
                    break;
                case "/":
                    this.value = childNodes[0].value / childNodes[1].value;
                    break;

                case "^":
                    this.value = Math.Pow(childNodes[0].value, childNodes[1].value);

                    break;
                case "sqrt":
                    if (childNodes[1].value < 0)
                    {
                        // Console.WriteLine("Error in Math_op in func eval in the switch case- sqrt of a negative number error. Bye Bye!");
                        //Environment.Exit(1);
                        childNodes[1].value *= -1;
                    }
                    //else
                        this.value = Math.Pow(childNodes[0].value, (childNodes[1].value)*-1);

                    break;

                default:
                    Console.WriteLine("Error in Intern in func eval in the switch case-not allowed operator error. Bye Bye!");
                    Environment.Exit(1);
                    break;
            }
        }

        public override void build_sons()//Sons of an intern node can be an operation/eval_func/const number
        {
            
            const int OPERATOR_PROB = 33;
            const int EVAL_FUNC_PROB = 33;
            const int IF_PROB = 33;
            this.num_of_nodes_under = 2;
            for (int i = 0; i < 2; i++)// +,-,*,/ are binary operators- always have 2 sons: 2+5, 20/(5*3)...
            {
                int choice = (GetRandom(0, 100));//33.333% to be an operation/eval_func/const number
                if (choice < OPERATOR_PROB)
                    choice = 0;
                else if (choice < OPERATOR_PROB+ EVAL_FUNC_PROB)
                    choice = 1;
                else
                    choice = 2;
                
                if (SIZE >= MAX_SIZE-1)
                    choice=(GetRandom(0, 100)%2) + 2;

                if (choice == 0)
                    (this.childNodes).Add(new Math_op());

                else if(choice==1)
                    (this.childNodes).Add(new If());

                else if (choice == 2)
                    (this.childNodes).Add(new Eval_func());

                else 
                    (this.childNodes).Add(new Const());

                
            }

        }

    }
}
