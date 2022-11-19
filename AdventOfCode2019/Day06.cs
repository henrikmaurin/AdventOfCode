using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day06 : DayBase, IDay
    {
        private List<string> orbitData;
        private List<Planet> planets;
        private List<Planet> you;
        private List<Planet> san;

        public Day06() : base(2019, 6)
        {
            orbitData = input.GetDataCached().SplitOnNewline();
            BuildMap();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of orbits: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of jumps: {result2}");
        }
        public int Problem1()
        {
            int total = 0;
            foreach (Planet planet in planets)
            {
                total += Traverse(planet);
            }

            return total;
        }

        public int Problem2()
        {
            you = new List<Planet>();
            san = new List<Planet>();

            Planet planetYou = planets.Where(p => p.Name == "YOU").Single();
            Planet planetSan = planets.Where(p => p.Name == "SAN").Single();

            Traverse(planetYou, "you");
            Traverse(planetSan, "san");

            int youcount = 0;
            int sancount = 0;

            foreach (Planet planet in you)
            {
                if (san.Select(p => p.Name).Contains(planet.Name))
                {
                    break;
                }
                youcount++;
            }

            foreach (Planet planet in san)
            {
                if (you.Select(p => p.Name).Contains(planet.Name))
                {
                    break;
                }
                sancount++;
            }

            return youcount + sancount - 2;
        }

        public int Traverse(Planet p, string buildFor = null)
        {
            if (buildFor == "you")
                you.Add(p);

            if (buildFor == "san")
                san.Add(p);

            if (p.orbits != null)
            {
                return 1 + Traverse(p.orbits, buildFor);
            }
            return 0;
        }

        public void BuildMap()
        {
            planets = new List<Planet>();

            foreach (string orbit in orbitData)
            {
                string planet1 = orbit.Split(")")[0];
                string planet2 = orbit.Split(")")[1];
                if (!planets.Where(p => p.Name == planet1).Any())
                    planets.Add(new Planet { Name = planet1 });
                if (!planets.Where(p => p.Name == planet2).Any())
                    planets.Add(new Planet { Name = planet2 });
                Planet p1 = planets.Where(p => p.Name == planet1).Single();
                Planet p2 = planets.Where(p => p.Name == planet2).Single();



                p2.orbits = p1;

            }


        }
    }

    public class Planet
    {
        public string Name;
        public Planet orbits = null;
    }
}
