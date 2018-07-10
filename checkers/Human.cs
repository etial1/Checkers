using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace checkers
{
    class Human : Player
    {
        Form1 form;
        public AMove stored;
        public override  AMove ChooseMove(AMove[] options, Board b, Form1 f, Player[] players,int depth)
        {
            this.form = f;
            b.PrintBoard2(f); //
            AMove ans;
            int f1=0, f2=0, t1=0, t2=0;
            string[] from, to;

            
            form.printMessageGui("Please enter your next move");
            while (form.getMoveIsClicked() == false)
            {
             
            }
            
            if(form.getMoveIsClicked()==true){
                form.setMoveIsClicked(false);
                from = form.getTextBox1().Split(' ');
                try
                {
                    f1 = int.Parse(from[0]);
                    f2 = int.Parse(from[1]);
                    to = form.getTextBox2().Split(' ');
                    t1 = int.Parse(to[0]);
                    t2 = int.Parse(to[1]);
                }
                catch(Exception invalid_input)
                    {
                        Console.WriteLine("invalid input");
                        ans = new AMove(-1, -1, -1, -1); //this move is guarenteed to fire the ilegal move catcher in game master
                        return ans;

                        
                    }
                form.clearTextBox();
                while (f1==null||f2==null||t1==null||t2==null||f1<0||f1>7&& f2 < 0 || f2 > 7 && t1 < 0 || t1 > 7 && t2 < 0 || t2 > 7)
                {
                    form.printMessageGui("Please enter a valid move!");
                    if (form.getMoveIsClicked() == true)
                    {
                        form.printMessageGui(" ");
                        form.setMoveIsClicked(false);
                         from = form.getTextBox1().Split(' ');
                         f1 = int.Parse(from[0]);
                         f2 = int.Parse(from[1]);
                         to = form.getTextBox2().Split(' ');
                         t1 = int.Parse(to[0]);
                         t2 = int.Parse(to[1]);
                        form.clearTextBox();
                    }

                }
               
                //System.Threading.Thread.Sleep(1000);
            }
            ans = new AMove(f1,f2,t1,t2);
            //ans = stored;
            //stored = null;
            return ans;

        }


       



    }
}
