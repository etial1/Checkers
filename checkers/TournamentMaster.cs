using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    class TournamentMaster
    {
        public Player[] contenders;// = new Player[100];
        public int iterations;
        private Form1 f;
        private GameMaster GM;
        Random rnd = new Random();

        public TournamentMaster(int size, int itr, Form1 frm)
        {

            contenders = new Player[size];
            for (int i = 0; i < size; i++)
            {
                contenders[i] = new Player(frm);
            }
            iterations = itr;
            f = frm;
            GM = new GameMaster(frm);
        }

        public Player Go(int size, int itr)
        {
            contenders = new Player[size];
            for (int i = 0; i < size; i++)
            {
                contenders[i] = new Player(f);
            }
            iterations = itr;

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("Round " + i + " ! FIGHT!");
                contenders = RoundV2(i);
            }
            contenders = OrderByRound(contenders);
            f.printMessageGui("The tournament is over!");
            return contenders[0];
        }

        public Player Go1(int size, int itr)
        {
            contenders = new Player[size];
            for (int i = 0; i < size; i++)
            {
                contenders[i] = new Player(f);
            }
            iterations = itr;

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("Round " + i + " ! FIGHT!");
                contenders = RoundV1(i);
            }
            contenders = OrderByRound(contenders);
            f.printMessageGui("The tournament is over!");
            return contenders[0];
        }



        public Player[] RoundV1(int round)
        {
            for (int i = 0; i < contenders.Length; i++)
            {
                contenders[i].wins = 0;
            }
            for (int i = 0; i < contenders.Length; i++)
            {
                for (int j = i; j < contenders.Length; j++)
                {
                    if (i != j)//dont play against yourself lol
                    {
                        Player p = GM.PvP(contenders[i], contenders[j], false);
                        if (p != null && p == contenders[i]) contenders[i].wins++; //whoevr wins gets a point
                        if (p != null && p == contenders[j]) contenders[j].wins++;
                        //if its a null its a tie

                    }
                }
            }
            Player[] ans = OrderByWins(contenders);
            ans = NextGenV1(ans);
            for (int i = 0; i < contenders.Length; i++)
            {
                contenders[i].wins = 0;
            }
            return ans;

        }

        public Player[] RoundV2(int round)
        {

            Player[] ans = NextGenV2(contenders, round);
            return ans;

        }

        public Player[] OrderByWins(Player[] arr)
        {
            Console.WriteLine();
            foreach (Player var in arr)
            {
                Console.Write(var.wins + "  ");
            }
            Console.WriteLine("win array before");
            Array.Sort(arr,
                delegate(Player x, Player y) { return y.wins.CompareTo(x.wins); });
            foreach (Player var in arr)
            {
                Console.Write(var.wins + "  ");
            }
            Console.WriteLine("win array after \n");
            return arr;
        }

        public Player[] OrderByRound(Player[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                group[i].wins = 0;
            }
            for (int i = 0; i < group.Length; i++)
            {
                for (int j = i; j < group.Length; j++)
                {
                    if (i != j)//dont play against yourself lol
                    {
                        group[i].id = 0;
                        group[i].id = 1;
                        Player p = GM.PvP(group[i], group[j], false);
                        if (p != null && p.id == group[i].id) group[i].wins++; //whoever wins gets a point
                        else if (p != null && p.id == group[j].id) group[j].wins++;
                        //if its a null its a tie

                    }
                }
            }
            Player[] ans = OrderByWins(group);
            for (int i = 0; i < group.Length; i++)
            {
                group[i].wins = 0;
            }
            return ans;
        }

        public Player[] NextGenV1(Player[] arr)
        {
            //TODO: test

            Player[] ans = new Player[contenders.Length];


            int all = contenders.Length;
            int top = (int)Math.Ceiling(contenders.Length * 0.1);
            int good = (int)Math.Ceiling(contenders.Length * 0.2) + top;
            int rest = all - good;

            Console.WriteLine("top " + top + " good " + good + " rest " + rest);
            for (int i = 0; i < all; i++)
            {
                ans[i] = new Player(f);
            }

            //ans = contenders;
            //chhose 5, best 2 chance cross chance mutation
            for (int i = good; i < all; i++)
            {
                int a = rnd.Next(all);
                int b = rnd.Next(good);
                ans[i].Plant(contenders[a].tree.mutation(contenders[b].tree));
            }

            for (int i = top; i < good; i++)
            {
                int a = rnd.Next(top);
                int b = rnd.Next(good);
                ans[i].Plant(contenders[a].tree.mutation(contenders[b].tree));
            }

            for (int i = 0; i < top; i++)
            {
                int a = rnd.Next(0, top);
                ans[i].Plant(contenders[a].tree.mutation(contenders[a].tree));
            }

            return ans;
        }

        public Player[] NextGenV2(Player[] arr, int round)
        {
            int mutationChance = 20;
            int crossChance = 70;
            int size = contenders.Length;
            Player[] ans = new Player[size];

            for (int i = 0; i < size; i++)
            {
                ans[i] = new Player(f);

                Player[] grouping = new Player[5];
                int[] chosenForGroup = new int[size];
                for (int j = 0; j < 5; j++)
                {
                    int b = rnd.Next(size);
                    if (chosenForGroup[b] == 0)
                    {
                        chosenForGroup[b] = 1;
                        grouping[j] = contenders[b];
                    }
                    else
                    {
                        Console.WriteLine(b + " picked already! ");
                        j--;
                    }
                }
                grouping = OrderByRound(grouping);

                Console.WriteLine("round " + round + " contender " + i + " chosen.");

                int a = rnd.Next(100);

                if (a < crossChance)
                    ans[i].Plant(grouping[0].tree.mutation(grouping[1].tree)); //cross
                else if (a < crossChance + mutationChance)
                    ans[i].Plant(grouping[0].tree.mutation(grouping[0].tree)); //mutation
                else ans[i] = grouping[0]; //elitism-ish

            }
            return ans;
        }


        /* public void tmp()
         {
             Board b=new Board();
             //b.Empty();

             //b.BoardArray[1, 0] = new Piece(2);
             //b.BoardArray[0, 1] = new Piece(1);

             Player player1 = new Player(f);
             player1.tree.print_tree(player1.tree);
             Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@");
             Node New = player1.tree.copy_tree(player1.tree,new Math_op());
             New.print_tree(New);

             /*Player player2 = new Player(f);
             player1.color = 1;
             player1.direction = 1;
             player2.color = 2;
             player2.direction = -1;

             for (int i = 0; i < 100; i++)
             {
                 Player player3=new Player(f);
                 player3.Plant(player1.tree.mutation(player2.tree));
                 Console.WriteLine(player3.tree==null);
             }
         }*/


        public void tmp()
        {
            Board b = new Board();
            //b.Empty();

            //b.BoardArray[1, 0] = new Piece(2);
            //b.BoardArray[0, 1] = new Piece(1);

            Player player1 = new Player(f);
            Player player2 = new Player(f);
            player1.color = 1;
            player1.direction = 1;
            player2.color = 2;
            player2.direction = -1;
            player1.tree.print_tree(player1.tree);
            Console.WriteLine("___________________________________________________________");
            player2.tree.print_tree(player2.tree);
            Console.WriteLine("___________________________________________________________");
            for (int i = 0; i < 1; i++)
            {
                Player player3 = new Player(f);
                player3.Plant(player1.tree.mutation(player2.tree));
                // Console.WriteLine(player3.tree == null);
                player3.tree.print_tree(player3.tree);
                Console.WriteLine("___________________________________________________________");

            }
        }

    }
}
