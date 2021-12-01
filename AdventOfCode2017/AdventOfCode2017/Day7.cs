using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day7 : AdventOfCode2017
    {
        public List<Tower> Towers { get; set; }
        public Day7()
        {
            string[] data = SplitLines(ReadData("7.txt"));
            Towers = new List<Tower>();

            foreach (string line in data)
            {
                string[] d = Tokenize(line);
                Tower newTower = new Tower();
                newTower.Name = d[0];
                newTower.Weight = int.Parse(d[1].Replace("(", "").Replace(")", ""));
                if (d.Count() > 2)
                {
                    for (int i = 3; i < d.Count(); i++)
                    {
                        newTower.TowerNames.Add(d[i].Replace(",", ""));
                    }
                }
                Towers.Add(newTower);
            }
            foreach (Tower t in Towers)
            {
                foreach (string towername in t.TowerNames)
                {
                    t.Towers.Add(Towers.Find(to => to.Name == towername));
                }
            }
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            foreach (Tower t in Towers)
            {
                if (t.TowerNames.Count > 0)
                {
                    foreach (string name in t.TowerNames)
                    {
                        Towers.Find(to => to.Name == name).NotBottom = true;
                    }
                }
            }

            Tower answer = Towers.Where(t => t.NotBottom == false).Single();
            Console.WriteLine($"Tower {answer.Name} is the bottom one");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            int[] unbalanced = Towers.Where(t => t.Unbalanced() != 0).Select(t => t.Unbalanced()).ToArray();


            Console.WriteLine($"Tower  is unbalanced, Correct weight should be {unbalanced.First()}");

        }


    }

    public class Tower
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<string> TowerNames { get; set; }
        public List<Tower> Towers { get; set; }
        public bool NotBottom { get; set; }
        public Tower()
        {
            TowerNames = new List<string>();
            Towers = new List<Tower>();
            NotBottom = false;
        }
        public int TotalWeight()
        {
            int weight = Weight;
            foreach (Tower tower in Towers)
            {
                weight += tower.TotalWeight();
            }

            return weight;
        }

        public int Unbalanced()
        {
            if (Towers.Count == 0)
            {
                return 0;
            }

            List<int> weights = Towers.Select(t => t.TotalWeight()).ToList();

            int maxw = weights.Max();
            int minw = weights.Min();


            if (maxw != minw)
            {
                var t = weights.GroupBy(w => w).Select(w => new { w.Key, Count = w.Count() });
                int wrongWeight = t.Where(a => a.Count == 1).Select(a => a.Key).Single();
                int rightWeight = t.Where(a => a.Count != 1).Select(a => a.Key).Distinct().Single();
                Tower tower = Towers.Where(b => b.TotalWeight() == wrongWeight).Single();

                return tower.Weight - (wrongWeight - rightWeight);
            }

            return 0;

        }

    }
}
