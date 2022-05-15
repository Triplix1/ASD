using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_6_32
{
    internal static class WorkWithTree
    {
        public static void GenerateTree(ref Tree root,int length)
        {
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (!Insert(ref root, rand.Next(15)))
                {
                    i--;
                }                
            }
        }
        public static bool Insert(ref Tree tree, int element)
        {
            if (tree == null)
            {
                tree = new Tree(element);
                return true;
            }
            if(tree.Value < element)
            {
                if (tree.left == null)
                {
                    tree.left = new Tree(element);
                    return true;
                }
                else
                {
                    return Insert(ref tree.left, element);
                }
            }
            else if (tree.Value > element)
            {
                if (tree.right == null)
                {
                    tree.right = new Tree(element);
                    return true;
                }
                else
                {
                    return Insert(ref tree.right, element);
                }
            }
            else
            {
                return false;
            }
        }

        static int Height(Tree tree)
        {
            if (tree == null)
            {
                return 0;
            }
            int h1 = 0, h2 = 0;
            if (tree.left != null)
            {
                h1 = Height(tree.left);
            }
            if (tree.right != null)
            {
                h2 = Height(tree.right);
            }

            if(h1 > h2)
            {
                return h1 + 1;
            }
            else
            {
                return h2 + 1;
            }
        }

        public static bool CheckBalanced(Tree tree)
        {
            if (tree == null)
            {
                return true;
            }
            int lh = Height(tree.left);
            int rh = Height(tree.right);
            if (Math.Abs(lh - rh) > 1) return false;
            bool lcheck = CheckBalanced(tree.left);
            bool rcheck = CheckBalanced(tree.right);

            return lcheck && rcheck;
        }

        public static void Output(Tree root, int n)
        {
            if (root != null)
            {
                Output(root.right, n + 5);
                for (int i = 0; i < n; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(root.Value);
                Output(root.left, n + 5);
            }
        }
    }
}
