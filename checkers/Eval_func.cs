using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class Eval_func:Leaf
    {
        List<string> eval_funcs = new List<string> { "FinishLineProx", "SoldierRatio", "QueenRatio", "Exposure" };

        public Eval_func()
        {
            this.sign = eval_funcs[GetRandom(0, 100) % 3];
            this.num_of_nodes_under = 0;

        }
        public Eval_func(Eval_func old)
        {
            this.sign = old.sign;
            this.num_of_nodes_under = old.num_of_nodes_under;

        }
        public override void eval()//Need to call to the eval func and return it's returned value;
        {
            this.num_of_nodes_under = 0;
            switch (this.sign) {

                case "FinishLineProx":
                    this.value = this.getResults()[0];
                    break;
                case "SoldierRatio":
                    this.value = this.getResults()[1];
                    break;
                case "QueenRatio":
                    this.value = this.getResults()[2];
                    break;
                case "Exposure":
                    this.value = this.getResults()[3];
                    break;


            }



        }
    }
}
