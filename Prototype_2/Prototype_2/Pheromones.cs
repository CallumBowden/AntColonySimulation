using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_2
{
    class Pheromones
    {
        public int X { set; get; }
        public int Y { set; get; }

        public double value { set; get; }

        public Pheromones(int x, int y)
        {
            // assign x and y value for nest
            X = x;
            Y = y;
            Console.WriteLine("pheromone placed at {0},{1} ", X, Y);
        }
    }
}
