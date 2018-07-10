using System.Collections.Generic;
using System;
namespace checkers
{
    public abstract class Node
    {
        public static int print_amount = 0;
        public int num_of_nodes_under = 0;
        public static int MAX_SIZE = 5;
        public static int SIZE = 1;
        public static int SIZE2 = 0;
        public static List<double> results;
        public string sign;
        public double value;
        public List<Node> childNodes;
        public Node father;
        abstract public void eval();
        public void eval_tree(Node cur)
        {

            if (cur.childNodes != null)
            {

                foreach (Node i in cur.childNodes)
                    if (i != null)
                    {
                        eval_tree(i);
                        cur.num_of_nodes_under += i.num_of_nodes_under;
                    }
                            
                    
                }
                cur.eval(); 
        }

        public double etiFunc(List<double> results)
        {
            //this.results = null;
           // this.results = new List<double>();
            setResults(results);
            eval_tree(this);
            return this.value; 
        }
        public static void setResults(List<double> results)
        {
            Node.results = results;
        }
        public List<double> getResults() { return Node.results; }

        abstract public void build_sons();
        public void print_tree(Node cur)
        {
            
           
            if (cur.GetType() == typeof(Leaf)|| cur.GetType() == typeof(Const) || cur.GetType() == typeof(Eval_func)) 
              return;
  
            if (cur.childNodes !=null)
            {
                Console.WriteLine("Father: " + cur.sign + " has " + (cur.childNodes).Count + " childes" + "total: " +cur.num_of_nodes_under);
        
                Console.WriteLine("childes: ");
                foreach (Node i in cur.childNodes)
                {
                    Console.WriteLine(i.sign);
                    print_tree(i);
                }
            }
          
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandom(int min, int max)
        {
            
             lock (syncLock)
            {  //synchronize

                if(min>max)
                {
                    int tmp = min;
                    min = max;
                    max = tmp;
                }
            return random.Next(min, max);
            }
        }
        public static class Util
        {
            
            public static int GetRandom()
            {
                return random.Next();
            }
        }
        public void bulid_tree(Node cur)
        {
            if (cur.GetType() == typeof(Leaf) || cur.GetType() == typeof(Const) || cur.GetType() == typeof(Eval_func))
            {
                cur.num_of_nodes_under=0;
                return;
            }
                

            if (cur.childNodes != null)
            {

                SIZE2 = 0;
                foreach (Node i in cur.childNodes)
                {
                    
                    SIZE2++;

                    i.build_sons();
                   
                    if (SIZE2 == ((cur.childNodes).Count)-1)
                        SIZE++;
                    bulid_tree(i);
                    cur.num_of_nodes_under += i.num_of_nodes_under;

                }
            }
                

        }

        /*   public Node mutation(Node second_tree)
           {
               //##########//Random place in seconde tree##########////
               Node runner = second_tree;
               Node runnerF = second_tree;
               int runnerR = 0;

               Node thisRunner = this;
               Node thisRunnerF = this;
               int thisRunnerR = 0;

               int dep = GetRandom(1, SIZE-1);//Until which depth to go

               //While there are childes
               while (runner.childNodes != null && runner.childNodes.Count != 0)  
               {

                   if (SIZE + dep-- <= SIZE)
                       break;
                   runnerF = runner;
                   //Go to random chiled
                   runnerR = GetRandom(0, runner.childNodes.Count);
                   runner = runner.childNodes[runnerR];
               }


               //##########Random place in this tree##########//
               dep = GetRandom(1, SIZE-1);
               //While there are childes
               while (thisRunner.childNodes!= null && thisRunner.childNodes.Count!=0)
               {

                   if (SIZE + dep-- <= SIZE)
                       break;
                   thisRunnerF = thisRunner;
                   //Go to random chiled
                   thisRunnerR = GetRandom(0, thisRunner.childNodes.Count);
                   thisRunner = thisRunner.childNodes[thisRunnerR];
               }

               //One of the roots are cond_node and one is not- need to fix this!
               if ( (thisRunner.GetType() == typeof(checkers.cond_node) || runner.GetType() == typeof(checkers.cond_node) ) && thisRunner.GetType() != runner.GetType())
                   if (thisRunner.GetType() == typeof(checkers.cond_node))
                       thisRunner = thisRunner.childNodes[GetRandom(0, thisRunner.childNodes.Count)];
                   else
                       runner = runner.childNodes[GetRandom(0, runner.childNodes.Count)];

               return Create_mutation_tree(runner, runnerF, thisRunner, thisRunnerF, runnerR, thisRunnerR);


           }*/

        public Node mutation(Node second_tree)
        {
            Node tree1 = copy_tree(this, new Math_op());
            Node tree2 = copy_tree(second_tree, new Math_op());
            Node tree1_runner = tree1;
            Node tree2_runner = tree2;
            return Create_mutation_tree(tree1, tree1_runner,tree2, tree2_runner);
        }

        public Node Create_mutation_tree(Node tree1, Node tree1_runner, Node tree2, Node tree2_runner)
        {
            bool flag = false;
            Node tree1_runner_father = null;
            Node tree2_runner_father = null;
            int tree1_runner_index = -1;
            int tree2_runner_index = -1;

            while (flag==false)
            {
                tree1_runner = tree1;
                tree2_runner = tree2;
                 tree1_runner_father = null;
                 tree2_runner_father = null;
                 tree1_runner_index=-1;
                 tree2_runner_index=-1;
                //run on both trees randomly
                int x = GetRandom(0, tree1_runner.num_of_nodes_under);
                for (int j = 0; j < tree1_runner.childNodes.Count; j++)
                    if (x == 0)
                        break;
                    else if (x > tree1_runner.childNodes[j].num_of_nodes_under + 1)
                        x -= tree1_runner.childNodes[j].num_of_nodes_under + 1;
                    else
                    {
                        x--;
                        tree1_runner_father = tree1_runner;
                        tree1_runner_index = j;
                        tree1_runner = tree1_runner.childNodes[j];
                        j = -1;
                        if (x == 0)
                            break;
                    }

                int y = GetRandom(0, tree2_runner.num_of_nodes_under);
                for (int j = 0; j < tree2_runner.childNodes.Count; j++)
                    if (y == 0)
                        break;
                    else if (y > tree2_runner.childNodes[j].num_of_nodes_under + 1)
                        y -= tree2_runner.childNodes[j].num_of_nodes_under + 1;
                    else
                    {
                        y--;
                        tree2_runner_father = tree2_runner;
                        tree2_runner_index = j;
                        tree2_runner = tree2_runner.childNodes[j];
                        j = -1;
                        if (y == 0)
                            break;
                    }

                //Check the two sub trees are mach to mutation (a leaf cant have childes for an example..)
                if (tree1_runner.GetType() != tree2_runner.GetType())
                    if (tree1_runner.GetType() == typeof(cond_node) || tree2_runner.GetType() == typeof(cond_node))
                        flag = false;
                    else flag = true;
                else flag = true;
            }

            //TO PRINY THE 2 CHOSEN SUB TREE'S
           /* Console.WriteLine("--------------------runner 1--------------------");
            if (tree1_runner.GetType() == typeof(Const) || tree1_runner.GetType() == typeof(Eval_func))
                Console.WriteLine(tree1_runner.sign);
            else tree1_runner.print_tree(tree1_runner);
            Console.WriteLine("--------------------runner 2--------------------");
            if (tree2_runner.GetType() == typeof(Const) || tree2_runner.GetType() == typeof(Eval_func))
                Console.WriteLine(tree2_runner.sign);
            else tree2_runner.print_tree(tree2_runner);*/
            

            //randomly chose which tree to conact to the another
            if (GetRandom(0, 100) % 2 == 0)
            {
                // Console.WriteLine("--------------------mutation tree 2 into tree 1--------------------");
                if (tree1_runner_father != null && tree1_runner_index != -1)
                    tree1_runner_father.childNodes[tree1_runner_index] = tree2_runner;
              
                tree1.update_num_of_nodes_under__after_mutation(tree1);
                return tree1;
            }
            else
            {
                //Console.WriteLine("--------------------mutation tree 1 into tree 2--------------------");
                if (tree2_runner_father != null && tree2_runner_index != -1)
                    tree2_runner_father.childNodes[tree2_runner_index] = tree1_runner;

                tree2.update_num_of_nodes_under__after_mutation(tree2);
                return tree2;
            }

        }
     

    public int update_num_of_nodes_under__after_mutation(Node a)
        {
                if (a.childNodes == null || a.childNodes.Count == 0)
                    return 0;

                a.num_of_nodes_under = a.childNodes.Count;
                foreach(Node i in a.childNodes)
                {
                     update_num_of_nodes_under__after_mutation(i);
                    a.num_of_nodes_under += i.num_of_nodes_under;
                }
            return 0;
        }

        public Node copy_tree(Node Old, Node New)
        {

            if (Old.GetType() == typeof(Const))
                New=(new Const((Const)Old));
 
            else if (Old.GetType() == typeof(Eval_func))
                New=(new  Eval_func((Eval_func)Old));

            else if (Old.GetType() == typeof(If))
                New=((new If((If)Old)));

            else if (Old.GetType() == typeof(Math_op))
                New=(new Math_op((Math_op)Old));

            else if(Old.GetType()==typeof(cond_node))
                New=(new cond_node((cond_node)Old));

            int amout_of_childes = Old.childNodes != null ? Old.childNodes.Count : 0;
           
            for(int j=0; j< amout_of_childes; j++)
                    New.childNodes.Add(copy_tree(Old.childNodes[j], new Math_op()));
                
            return New;
        }
    }
    
}

