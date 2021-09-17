using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactica
{
    class Star : SpaceObject
    {
        public StarType Type { get; set; }
        public float Temperature { get; set; }
        public List<Planet> Planets { get; set; }

        new public Position Position { get; }

        public Star()
        {
            this.Position = new Position(0f, 0f);
            Planets = new List<Planet>();
        }
    }

    enum StarType
    {
        YellowDwarf,
        White,
        BlueNeutron,
        RedGiant
    }
}
