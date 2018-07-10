using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Piece
    {
        public int color;
        //public char repersentation;

        public Piece(int c)
        {
            color = c;
        }
        public Piece()
        {
            color = 0;
        }
    }
}
