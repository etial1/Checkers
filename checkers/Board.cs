using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
   class Board
    {
        public Piece[,] BoardArray = new Piece[8, 8];  //board[x,y]
        public Form1 myForm;

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    BoardArray[i, j] = null;
                    if (i%2 != j%2)
                    {
                        if (j < 3) BoardArray[i, j] = new Piece(1);
                        if (j > 4) BoardArray[i, j] = new Piece(2);
                    }
                }
            }
                
        }

        //copy ctor
        public Board(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.BoardArray[i, j] = board.BoardArray[i, j];
                }
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoardArray[j, i] == null)
                        Console.Write("0 "); //empty tile
                    else
                    {
                        if (BoardArray[j, i] is Queen)
                        {
                            if ((BoardArray[j, i].color) == 1) Console.Write("# "); //player 1 queen
                            else Console.Write("? "); //player 2 queen

                        }
                        else Console.Write(BoardArray[j, i].color+" "); //print 1 for player1 piece, 2 for 2
                    }
                    
                }
            Console.WriteLine();
            }
        }

        public void PrintBoard2(Form1 f)
        { 
            var form = Form.ActiveForm as Form1;
            string ans = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoardArray[j, i] == null)
                        ans += "0 ";
                    else
                    {
                        if (BoardArray[j, i] is Queen)
                        {
                            if ((BoardArray[j, i].color) == 1)  ans += "# "; //player 1 queen
                            else  ans += "? "; //player 2 queen

                        }
                        else  ans +=  BoardArray[j, i].color + " "; //print 1 for player1 piece, 2 for 2
                    }

                }
                 ans += "\n";
            }
            ans = ans.Substring(0, ans.Length - 2); //remove the /n of the last line
            f.Print(ans);
        }

        public double FinishLineProx(Player p,Board b)
        {
            //distance between closest soldier to finish line
            double ans = 8; //default answer.
            if (p.direction == 1)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 7; j > 0; j--)
                    {
                        if (b.BoardArray[i, j] != null && b.BoardArray[i, j].color == p.color &&
                            b.BoardArray[i, j].GetType() == typeof (Piece))
                            //find the soldier closest to the enemy starting line
                            if (ans > 7 - j) ans = 7 - j;
                    }
                return ans;
            }
            else
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (b.BoardArray[i, j] != null && b.BoardArray[i, j].color == p.color &&
                            b.BoardArray[i, j].GetType() == typeof (Piece))
                            //find the soldier closest to the enemy starting line
                            if (ans > j) ans = j;
                    }
                return ans;
            }
        }

        public double SoldierRatio(Player p, Board b)
        {//returns ratio between player soldiers to eemy soldiers
            int c1 = 0; //player counter
            int c2 = 0; //enemy counter
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (b.BoardArray[i, j] != null && b.BoardArray[i, j].GetType() == typeof (Piece))
                    {
                        if (b.BoardArray[i, j].color == p.color) c1++;
                        else c2++;
                    }
                }
            }
            if (c2 != 0)
                return (double)c1 / c2;
            return int.MaxValue;
        }

        public double QueenRatio(Player p, Board b)
        {//returns ratio between player soldiers to eemy soldiers
            int c1 = 0; //player counter
            int c2 = 0; //enemy counter
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (b.BoardArray[i, j] != null && b.BoardArray[i, j].GetType() == typeof (Queen))
                    {
                        if (b.BoardArray[i, j].color == p.color) c1++;
                        else c2++;
                    }
                }
            }
            if (c2 != 0)
                return (double) c1/c2;
            return int.MaxValue;
        }

        public double Exposure(Player p, Board b)
        {
            //number of empty squeres directly behind a player piece
            double c1 = 0; //counter.
            if (p.direction == 1)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 7; j >= 0; j--)
                    {
                        if (b.BoardArray[i, j] != null && b.BoardArray[i, j].color == p.color)
                        {
                            if (i+1 >=0 && i+1<=7)
                                if (j - p.direction >= 0 && j - p.direction <= 7 && b.BoardArray[i+1, j-p.direction] == null) c1++;
                            if (i - 1 >= 0 && i - 1 <= 7)
                                if (j - p.direction >= 0 && j - p.direction <= 7 && b.BoardArray[i - 1, j - p.direction] == null) c1++;
                        }
                            
                    }
              
            }
            if (p.direction == -1)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 7; j >= 0; j--)
                    {
                        if (b.BoardArray[i, j] != null && b.BoardArray[i, j].color == p.color)
                        {
                            if (i + 1 >= 0 && i + 1 <= 7)
                                if (j - p.direction >= 0 && j - p.direction <= 7 && b.BoardArray[i + 1, j - p.direction] == null) c1++;
                            if (i - 1 >= 0 && i - 1 <= 7)
                                if (j - p.direction >= 0 && j - p.direction <= 7 && b.BoardArray[i - 1, j - p.direction] == null) c1++;
                        }

                    }
            }
            return c1;
        }

       public void Empty()
       {
           for (int i = 0; i < 8; i++)
           {
               for (int j = 0; j < 8; j++)
               {
                   BoardArray[i, j] = null;
                   
               }
           }
       }


    }

    
}
