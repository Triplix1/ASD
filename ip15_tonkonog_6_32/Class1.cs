using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_6_32
{
    internal class Tree
    {        
        public int Value { get; set; }
        public Tree left;
        public Tree right;
        public Tree(int value)
        {
            Value = value;
        }
    }
}
