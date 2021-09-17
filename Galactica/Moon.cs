using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactica
{
    sealed class Moon : Planet
    {
        public Planet Orbiting { get; set; }

        public override float Distance
        {
            get
            {
                return Methods.Distance(this, Orbiting);
            }
        }
    }
}
