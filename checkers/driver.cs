using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycheckers
{
   public class driver
    {
        public static Node create_new_tree()
        {   mycheckers.Node.SIZE = 0;
            Math_op root = new Math_op();
            
            root.childNodes = new List<Node>();
            root.build_sons();
            root.bulid_tree(root);
            root.print_tree(root);
            Console.WriteLine("================================================  " + mycheckers.Node.SIZE + " LEVELS  ================================================  ");
            Console.WriteLine("=============================>>>> Total Calculate: " + root.value + " <<<<=============================");
            root.eval_tree(root);

            return root;
        }

        public static void Main()
        {
            Node root = create_new_tree();
            Node root2 = create_new_tree();
            Node born=root.mutation(root2);
            born.eval_tree(born);
            born.print_tree(born);
            Console.WriteLine("================================================  " + mycheckers.Node.SIZE + " LEVELS  ================================================  ");
            Console.WriteLine("=============================>>>> Total Calculate: " + born.value + " <<<<=============================");
            
        }

      
        
    }
}
