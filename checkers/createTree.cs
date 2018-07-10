using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{

   public class createTree
    {


        public static Node buildTree()
        {
            checkers.Node.SIZE = 0;
            Math_op root = new Math_op();

            root.childNodes = new List<Node>();
            root.build_sons();
            root.bulid_tree(root);
            return root;
        }
 
    }
}
