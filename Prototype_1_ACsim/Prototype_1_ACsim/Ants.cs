using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_1_ACsim
{
    class Ants
    {
        public int X { set; get; }
        public int Y { set; get; }

        public bool isCarrying { set; get; }
        public bool isCloseToNest { set; get; }
        public bool isCloseToFood { set; get; }
        public int closestAnt { set; get; }
        public bool isCloseToAnt { set; get; }

        public bool foodLocationKnown { set; get; }
        public int foodClosest { set; get; }

        public bool nestLocationKnown { set; get; }
        public int nestClosest { set; get; }

        private Random randomObj;

        public Ants(Random rand)
        {
            // assign x and y values for ants
            randomObj = rand;

            X = randomObj.Next(0, 806);
            Y = randomObj.Next(0, 430);

            Console.WriteLine("ant created at {0},{1} ", X, Y);
        }
    }
}
