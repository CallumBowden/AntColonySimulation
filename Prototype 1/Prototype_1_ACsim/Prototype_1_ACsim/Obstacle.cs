using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_1_ACsim
{
    class Obstacle // ants must go round this
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Size { set; get; }

        public Obstacle(int x, int y)
        {
            // x + y location for obstacle
            X = x;
            Y = y;
            Console.WriteLine("Obstacle created at {0},{1} ", X, Y);
        }
    }
}
