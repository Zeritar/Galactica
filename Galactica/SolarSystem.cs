using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactica
{
    class SolarSystem
    {
        Methods methods;
        public int StarCount { get; set; }
        public int PlanetCount { get; set; }
        public int MoonCount { get; set; }
        public List<Star> Stars { get; set; }
        public int HoursPerTick { get; set; }

        public int Ticks { get; set; }
        public SolarSystem(Methods methods, int hoursPerTick)
        {
            HoursPerTick = hoursPerTick;
            Ticks = 0;
            Stars = new List<Star>();
            this.methods = methods;
        }

        public void PopulateSolarSystem()
        {
            // Sun
            methods.CreateStar("Sun", 5504.85f, StarType.YellowDwarf);

            // Planets
            List<Planet> planets = new List<Planet>()
            {
                methods.CreatePlanet("Mercury", PlanetType.Terrestial, new Position(46, 0), 4880000, 1407, 176),
                methods.CreatePlanet("Venus", PlanetType.Terrestial, new Position(107.5f, 0), 12104000, 5832, 225),
                methods.CreatePlanet("Earth", PlanetType.Terrestial, new Position(150.4f, 0), 12742000, 24, 365),
                methods.CreatePlanet("Mars", PlanetType.Terrestial, new Position(247.56f, 0), 6779000, 24.6f, 780),
                methods.CreatePlanet("Jupiter", PlanetType.Gas_Giant, new Position(751.01f, 0), 139822000, 10, 4332),
                methods.CreatePlanet("Saturn", PlanetType.Gas_Giant, new Position(1484.7f, 0), 116464000, 10.5f, 10759),
                methods.CreatePlanet("Uranus", PlanetType.Giant, new Position(2952.1f, 0), 50724000, 17, 30688),
                methods.CreatePlanet("Neptune", PlanetType.Giant, new Position(4475.1f, 0), 49244000, 16, 60195)

            };

            // Luna, orbits Earth
            planets[2].Moons.Add(
                methods.CreateMoon("Luna", PlanetType.Dwarf, planets[2], new Position(0.384f, 0), 3474000, 708, 29.5f)
            );

            // Titan, orbits Saturn
            planets[5].Moons.Add(
                methods.CreateMoon("Titan", PlanetType.Giant, planets[5], new Position(1.222f, 0), 5148000, 384, 16)
            );

            // Phobos, orbits Mars
            planets[3].Moons.Add(
                methods.CreateMoon("Phobos", PlanetType.Dwarf, planets[3], new Position(0.006f, 0), 22000, 180, 7.5f)
            );

            // Europa, orbits Jupiter
            planets[4].Moons.Add(
                methods.CreateMoon("Europa", PlanetType.Giant, planets[4], new Position(0.670f, 0), 3120000, 84, 3.5f)
            );

            // Deimos, orbits Mars
            planets[3].Moons.Add(
                methods.CreateMoon("Deimos", PlanetType.Dwarf, planets[3], new Position(0.023f, 0), 12000, 28.8f, 1.2f)
            );

            // Ganymede, orbits Jupiter
            planets[4].Moons.Add(
                methods.CreateMoon("Ganymede", PlanetType.Giant, planets[4], new Position(1.070f, 0), 5268000, 171.6f, 7.15f)
            );

            // Io, orbits Jupiter
            planets[4].Moons.Add(
                methods.CreateMoon("Io", PlanetType.Dwarf, planets[4], new Position(0.422f, 0), 3642000, 42.48f, 1.77f)
            );

            // Mimas, orbits Saturn
            planets[5].Moons.Add(
                methods.CreateMoon("Mimas", PlanetType.Dwarf, planets[5], new Position(0.186f, 0), 396000, 22.56f, 0.94f)
            );

            Stars[0].Planets = planets;
        }
    }
}
