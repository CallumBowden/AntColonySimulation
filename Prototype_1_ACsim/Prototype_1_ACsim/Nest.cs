using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_1_ACsim
{
    class Nest
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Nest(int x, int y)
        {
            // assign x and y value for nest
            X = x;
            Y = y;
            Console.WriteLine("Nest created at {0},{1} ", X, Y);
        }
    }
}
