using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_8_32_2_
{
    internal class Coord
    {
        int x;
        int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public double GetDistance(Coord other)
        {
            return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));
        }
    }
}
