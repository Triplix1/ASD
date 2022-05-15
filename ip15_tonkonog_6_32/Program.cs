using System;
using static ip15_tonkonog_6_32.WorkWithTree;

namespace ip15_tonkonog_6_32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Tree root = null;
                GenerateTree(ref root, 8);
                Console.WriteLine(CheckBalanced(root));
                Output(root, 0);
                Console.WriteLine();
            }            
        }
    }
}
