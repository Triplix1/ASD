using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_8_32_2_
{
    internal class Ant
    {
        int[] trail;
        public Ant(int[] trail)
        {
            this.trail = trail;
        }
        public int[] Trail { get { return trail; } }
    }
}
