using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_2
{
    class Ants
    {
        public int X { set; get; }
        public int Y { set; get; }

        public bool isCarrying { set; get; }
        public bool isCloseToNest { set; get; }
        public bool isCloseToFood { set; get; }

        public bool destinationNest { set; get; } // so aco isnt run on every ant, on every tick 
        public int DestNestID { set; get; }
        
        public bool foodLocationKnown { set; get; }
        public int foodClosest { set; get; }
        public int foodSourceID { set; get; } // sets id of source food was picked up from - for randomMove_Carrying

        public bool nestLocationKnown { set; get; }
        public int nestClosest { set; get; }

        public bool Smart_Ant { set; get; }
        public bool followP { set; get; } // if ant decides to follow P, this is set to true an probability of not following P next move is reduced

        private Random randomObj;

        public Ants(Random rand)
        {
            // assign x and y values for ants
            randomObj = rand;

            X = randomObj.Next(1, 762);
            Y = randomObj.Next(1, 415);

            Console.WriteLine("ant created at {0},{1} ", X, Y);
        }
    }
}
