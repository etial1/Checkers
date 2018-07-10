using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class AMove
    {
        public int[] From = new int[2];
        public int[] To = new int[2];

        public AMove(int f1, int f2, int t1, int t2)
        {
            From = new int[] { f1, f2 };
            To = new int[] { t1, t2 };
        }

        public void printAMove()
        {
            Console.WriteLine("(" + From[0] + "," + From[1] + ") => (" + To[0] + "," + To[1] + ")");
        }
    }

}
