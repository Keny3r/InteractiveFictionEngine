using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Actions
{
    internal class Direction
    {
        public static List<string> Directions;

        static Direction()
        {
            Directions = new()
            {
                "north",
                "south",
                "east",
                "west",
                "down",
                "up",
                "inside",
                "outside"
            };
        }
    }
}
