using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_2
{
    class Food
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Quantity { set; get; } // volume of food in pile

        public Food(int x, int y)
        {
            // assign x and y values for food location
            X = x;
            Y = y;
            Console.WriteLine("Food created at {0},{1} ", X, Y);
        }
    }
}
