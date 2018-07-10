using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class If : Intern
    {
       
        public If()
        {
           
            this.childNodes = new List<Node>();
            this.sign = "IF";
            this.num_of_nodes_under = 3;
        }

        public If(If old)
        {

            this.childNodes = new List<Node>();
            this.sign = old.sign;
            this.num_of_nodes_under = old.num_of_nodes_under;
        }
        public override void eval()
        {
            this.num_of_nodes_under = 3;
            if (((this.childNodes)[0].value) == 1)
                this.value = (this.childNodes)[1].value;
            else
                this.value = (this.childNodes)[2].value;

        }

        public override void build_sons()//if has 3 sons: childNodes[0]- cond, childNodes[1]- if cond true  else childNodes[2]
        {

            /*const int OPERATOR_PROB = 33;
            const int EVAL_FUNC_PROB = 33;
            const int IF_PROB = 33;*/
            this.num_of_nodes_under = 3;
            (this.childNodes).Add(new cond_node());
            int flag, choice;
            flag = 0;

            for (int i = 0; i < 2; i++)
            {
                if (SIZE >= MAX_SIZE-1)
                   flag = 1;

                if (i == 0)
                    choice = 0;//The left son will be an operator because we need a boolean statment
                else
                {
                     if (flag == 0)
                        choice = ((GetRandom(0, 100)) % 3) + 1;//Not cond, the tree size is fine
                    else
                         choice = ((GetRandom(0, 100)) % 2) + 2;//Not cond, the tree size is not fine- only build leafs
                }
                   

                if(choice == 1)
                    (this.childNodes).Add(new Math_op());

                else if (choice == 2)
                    (this.childNodes).Add(new Eval_func());

                else
                    (this.childNodes).Add(new Const());
            }

        }
    }
}
