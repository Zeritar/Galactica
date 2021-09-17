using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Galactica
{
    class Methods
    {
        SolarSystem solarSystem;
        public Methods ()
        {
            solarSystem = new SolarSystem(this, 240);
            solarSystem.PopulateSolarSystem();
        }
        public static float Distance(Planet planet)
        {
            return MathF.Sqrt(
                (MathF.Pow(0 - planet.Position.X, 2) + MathF.Pow(0 - planet.Position.Y, 2))
                );
        }

        public static float Distance(Moon moon, Planet planet)
        {
            return MathF.Sqrt(
                (MathF.Pow(planet.Position.X - moon.Position.X, 2) + MathF.Pow(moon.Position.Y - moon.Position.Y, 2))
                );
        }

        public void CreateStar(string name, float temperature, StarType type)
        {
            solarSystem.StarCount++;
            solarSystem.Stars.Add(new Star()
            {
                Id = solarSystem.StarCount,
                Name = name,
                Temperature = temperature,
                Type = type
            });
        }

        public Planet CreatePlanet(string name, PlanetType type, Position position, int diameter, float rotationHours, float revolutionDays)
        {
            solarSystem.PlanetCount++;
            Planet planet = new Planet()
            {
                Id = solarSystem.PlanetCount,
                Name = name,
                DiameterInMeters = diameter,
                Type = type,
                RotationPeriodInHours = rotationHours,
                RevolutionPeriodInDays = revolutionDays,
                Position = position
            };

            return planet;
        }

        public Moon CreateMoon(string name, PlanetType type, Planet orbitingPlanet, Position position, int diameter, float rotationHours, float revolutionDays)
        {
            solarSystem.PlanetCount++;
            Moon moon = new Moon()
            {
                Id = solarSystem.PlanetCount,
                Name = name,
                DiameterInMeters = diameter,
                Type = type,
                RotationPeriodInHours = rotationHours,
                RevolutionPeriodInDays = revolutionDays,
                Position = orbitingPlanet.Position + position,
                Orbiting = orbitingPlanet
            };

            return moon;
        }

        enum CelestialBody
        {
            Star,
            Planet,
            Moon
        }

        float drawscaleX = 4475 / Console.WindowWidth * 2;
        float drawscaleY = 4475 / Console.WindowHeight * 2;

        void DrawCharacters(string symbol, Position position)
        {
            Position drawPos = new Position(Console.WindowWidth / 2 + position.X, Console.WindowHeight / 2 + position.Y);
            int xMargin = -2;
            int yMargin = -2;

            if (position.X < 0)
                xMargin *= -1;
            if (position.Y < 0)
                yMargin *= -1;
            Console.SetCursorPosition((int)drawPos.X + xMargin, (int)drawPos.Y + yMargin);
            Console.Write(symbol);
        }

        void DrawCelestialBody(CelestialBody body, Position? position)
        {
            switch (body)
            {
                case CelestialBody.Star:
                    DrawCharacters("@", new Position(0, 0));
                    break;
                case CelestialBody.Planet:
                    DrawCharacters("*", new Position(position.X / drawscaleX, position.Y / drawscaleY));
                    break;
                case CelestialBody.Moon:
                    DrawCharacters(".", new Position(position.X / drawscaleX, position.Y / drawscaleY));
                    break;
            }
        }

        void DrawLabel(string name, Position position)
        {
            Position drawPos = new Position(Console.WindowWidth / 2 + position.X, Console.WindowHeight / 2 + position.Y);
            int xMargin = -name.Length;
            int yMargin = -3;

            if (position.X < 0)
                xMargin *= -1;
            if (position.Y < 0)
                yMargin *= -1;

            Console.SetCursorPosition((int)drawPos.X + xMargin, (int)drawPos.Y + yMargin);
            Console.Write(name);
        }

        public void DrawSolarSystem(SolarSystem solarSystem)
        {
            foreach (Planet planet in solarSystem.Stars[0].Planets)
            {
                DrawCelestialBody(CelestialBody.Planet, planet.Position);
                DrawLabel(planet.Name, new Position(planet.Position.X / drawscaleX, planet.Position.Y / drawscaleY));
            }
            DrawCelestialBody(CelestialBody.Star, null);
        }

        public void OrbitTicker_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            foreach (Planet planet in solarSystem.Stars[0].Planets)
            {
                Console.WriteLine($"{planet.Name} - {planet.Distance.ToString("0.00")} million km from the Sun");

                foreach (Moon moon in planet.Moons)
                {
                    Console.WriteLine($"- {moon.Name} - {moon.Distance.ToString("0.00")} million km from {planet.Name}");
                }
                Console.WriteLine();

            }
            solarSystem.Ticks++;
            int hoursElapsed = solarSystem.HoursPerTick * solarSystem.Ticks;
            foreach(Planet planet in solarSystem.Stars[0].Planets)
            {
                Position oldPos = planet.Position;

                float degrees = GetDegreesFromOrbit(planet.RevolutionPeriodInDays, hoursElapsed);
                planet.Position = CalculateOrbitPosition(planet.Distance, DegreesToUnitPoint(degrees));

                Position offset = planet.Position - oldPos;

                foreach (Moon moon in planet.Moons)
                {
                    moon.Position += offset;
                    // degrees = GetDegreesFromOrbit(moon.RevolutionPeriodInDays, hoursElapsed);
                    moon.Position = CalculateOrbitPosition(moon.Distance, DegreesToUnitPoint(0), planet);
                }
            }
            DrawSolarSystem(solarSystem);
        }

        public float GetDegreesFromOrbit(float revdays, float hoursElapsed)
        {
            float degreesPerHour = 360 / revdays / 24;
            float degrees = (degreesPerHour * hoursElapsed) % 360;

            return degrees;
        }

        public Position DegreesToUnitPoint(float degrees)
        {
            float rad = degrees * MathF.PI / 180;
            float x = MathF.Sin(rad);
            float y = MathF.Cos(rad);

            return new Position(x, y);
        }

        public Position CalculateOrbitPosition(float distance, Position unitPoint)
        {
            return unitPoint * distance;
        }

        public Position CalculateOrbitPosition(float distance, Position unitPoint, Planet planet)
        {
            return (unitPoint * distance) + planet.Position;
        }
    }
}
