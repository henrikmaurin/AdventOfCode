using Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AdventOfCode2018.Day24;

namespace AdventOfCode2018
{
    public class Day15 : DayBase, IDay
    {
        private const int day = 15;
        private string[] data;

        public Map2D<int> Map { get; set; }
        //public int[,] WorkingMap { get; set; }
        public List<Combatant> Combatants { get; set; }
        public int xSize { get; private set; }
        public int ySize { get; private set; }
        //public List<Coord> targets { get; private set; }
        public bool DoDumpMap { get; private set; } = false;

        public Day15(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();

        }

        public void InitMap()
        {
            xSize = data[0].Length;
            ySize = data.Length;

            Combatants = new List<Combatant>();

            Map = new Map2D<int>();
            Map.Init(xSize, ySize, 0);

            foreach (Vector2D coord in Map.EnumerateCoords())
            {
                switch (data[coord.Y][coord.X])
                {
                    case '#':
                        Map[coord] = int.MaxValue;
                        break;
                    case 'E':
                    case 'G':
                        Combatant p = new Combatant { Position = coord, Type = data[coord.Y][coord.X], HP = 200, Dead = false };
                        Map[coord] = 0;
                        Combatants.Add(p);
                        break;
                }
            }

            DumpMap(Map, "Initial.txt");
        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }
        public string Problem1()
        {
            InitMap();
            bool done = false;
            int rounds = 0;

            DumpMapLarge(Map, "Large_init.txt");
            Map2D<int> WorkingMap = new Map2D<int>();

            while (!done)
            {
                done = BattleRound();
                DumpMap(Map, $"Round{rounds}.txt");


                if (done)
                {
                    int remainingHP = Combatants.Sum(co => co.HP);


                    Console.WriteLine($"Done after {rounds} rounds, Remaining HP: {remainingHP}, Answer: {rounds * remainingHP} ");
                }
                rounds++;
            }
            return string.Empty;
        }


        public int Problem2(int ElfPower = 3)
        {
            InitMap();
            bool flawlessVictory = false;
            int elfCount = Combatants.Where(e => e.Type == 'E').Count();
            int elfPower = 3;
            while (!flawlessVictory)
            {
                InitMap();
                bool done = false;
                int rounds = 0;

                DumpMapLarge(Map, "Large_init.txt");
                Map2D<int> WorkingMap = new Map2D<int>();

                while (!done)
                {
                    done = BattleRound(elfPower);
                    DumpMap(Map, $"Round{rounds}.txt");


                    if (done)
                    {
                        if (Combatants.Where(e => e.Type == 'E' && e.Dead).Count() == 0)
                        {
                            int remainingHP = Combatants.Sum(co => co.HP);
                            Console.WriteLine($"Done after {rounds} rounds, Remaining HP: {remainingHP}, Answer: {rounds * remainingHP} ");
                            flawlessVictory = true;
                            return rounds * remainingHP;

                        }
                    }
                    rounds++;
                }
                elfPower++;
            }
            return int.MaxValue;
        }

        public bool BattleRound(int elfPower = 3)
        {
            List<Combatant> remaining = GetRemaingCombatants();
            bool done = false;

            foreach (Combatant combatant in remaining)
            {
                // Could die during round
                if (!combatant.Dead)
                {
                    List<Combatant> adjacentEnemies = FindAdjacent(combatant);

                    if (adjacentEnemies.Count == 0)
                    {
                        Vector2D nextMove = GetNextStep(combatant);
                        combatant.Position = nextMove;
                    }

                    adjacentEnemies = FindAdjacent(combatant);

                    if (adjacentEnemies.Count > 0)
                    {
                        AttackEnemy(combatant, adjacentEnemies.First(), elfPower);
                        if (!Combatants.Any(e => e.Type != combatant.Type && !e.Dead))
                        {
                            int remainingHP = Combatants.Sum(co => co.HP);
                            if (combatant == remaining.Where(a => !a.Dead).Last())
                            {

                            }
                            DumpMap(Map, "End.txt");
                            done = true;
                        }
                    }
                }
                //DumpMap(Map, $"Round1.txt");
                ///Console.ReadKey();
            }
            return done;
        }

        public List<Combatant> FindAdjacent(Combatant combatant)
        {
            return Combatants.Where(e => e.Type != combatant.Type)
                             .Where(e => !e.Dead)
                             .Where(e => e.Position.In(Directions.GetNeighboringCoordsFor(combatant.Position)))
                             .OrderBy(e => e.HP)
                             .ThenBy(e => e.Position.Y)
                             .ThenBy(e => e.Position.X)
                             .ToList();
        }

        public List<Coord> FindTargets(Combatant combatant)
        {
            List<Coord> targets = new List<Coord>();
            foreach (Combatant enemy in Combatants.Where(e => e.Type != combatant.Type && !e.Dead && e != combatant))
            {
                foreach (Vector2D position in Directions.GetNeighboringCoordsFor(enemy.Position))
                {
                    if (Map[position] != 0)
                        continue;
                    if (Combatants.Where(c => c.Position.Equals(position) && !c.Dead).Any())
                        continue;
                    if (targets.Any(t => t.Equals(enemy.Position)))
                        continue;

                    targets.Add(new Coord { X = position.X, Y = position.Y });
                }
            }
            return targets;
        }

        public List<Coord> FindDistance(List<Coord> targets, Map2D<int> distances)
        {
            foreach (Coord target in targets)
            {
                target.Distance = distances[target];
            }
            return targets.Where(t => t.Distance > 0).OrderBy(t => t.Distance).ToList();
        }

        public Vector2D GetNextStep(Combatant combatant)
        {
            List<Coord> targets = FindTargets(combatant);
            Map2D<int> targetMap = ClumsyMap(combatant.Position.X, combatant.Position.Y);
            targets = FindDistance(targets, targetMap);
            if (targets.Count > 0)
            {
                int minDistance = targets.Select(x => x.Distance).Min();
                Coord target = targets.Where(t => t.Distance == minDistance).OrderBy(t => t.Y).ThenBy(t => t.X).First();
                var track = Backtrack(target.X, target.Y, targetMap);
                return track.Last();
            }

            return combatant.Position;
        }
    
        public void AttackEnemy(Combatant combatant, Combatant enemy, int elfPower = 3)
        {
            if (enemy != null)
            {
                if (enemy.Position.In(Directions.GetNeighboringCoordsFor(combatant.Position)))
                {
                    if (enemy.Type == 'G')
                    {
                        enemy.HP -= elfPower;
                    }
                    else
                    {
                        enemy.HP -= 3;
                    }
                    if (enemy.HP <= 0)
                    {
                        enemy.Dead = true;
                        enemy.HP = 0;

                    }
                }
            }
        }

        public List<Coord> Backtrack(int x, int y, Map2D<int> map)
        {
            List<Coord> track = new List<Coord>();
            track.Add(new Coord { X = x, Y = y });
            int currentVal = map[x, y];
            while (currentVal > 1)
            {
                if (map[x, y - 1] == currentVal - 1)
                {
                    y--;
                }
                else if (map[x + 1, y] == currentVal - 1)
                {
                    x++;
                }
                else if (map[x - 1, y] == currentVal - 1)
                {
                    x--;
                }
                else if (map[x, y + 1] == currentVal - 1)
                {
                    y++;
                }
                track.Add(new Coord { X = x, Y = y });
                currentVal--;
            }
            return track;
        }

        public Map2D<int> ClumsyMap(int xpos, int ypos)
        {
            int changes = 1;
            Map2D<int> map = new Map2D<int>();
            map.Init(xSize, ySize);


            foreach (Combatant c in Combatants.Where(c => !c.Dead))
            {
                map[c.Position.X, c.Position.Y] = int.MaxValue;
            }

            if (map[xpos - 1, ypos] == 0 && Map[xpos - 1, ypos] != int.MaxValue)
            {
                map[xpos - 1, ypos] = 1;
            }

            if (map[xpos + 1, ypos] == 0 && Map[xpos + 1, ypos] != int.MaxValue)
            {
                map[xpos + 1, ypos] = 1;
            }

            if (map[xpos, ypos - 1] == 0 && Map[xpos, ypos - 1] != int.MaxValue)
            {
                map[xpos, ypos - 1] = 1;
            }

            if (map[xpos, ypos + 1] == 0 && Map[xpos, ypos + 1] != int.MaxValue)
            {
                map[xpos, ypos + 1] = 1;
            }
            int range = 0;
            while (changes != 0)
            {
                range++;
                changes = 0;

                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        if (Map[x, y] == int.MaxValue)
                        {
                            map[x, y] = int.MaxValue;
                        }
                        else if (map[x, y] == range)
                        {
                            if (map[x - 1, y] == 0)
                            {
                                map[x - 1, y] = range + 1;
                                changes++;
                            }

                            if (map[x + 1, y] == 0)
                            {
                                map[x + 1, y] = range + 1;
                                changes++;
                            }

                            if (map[x, y - 1] == 0)
                            {
                                map[x, y - 1] = range + 1;
                                changes++;
                            }

                            if (map[x, y + 1] == 0)
                            {
                                map[x, y + 1] = range + 1;
                                changes++;
                            }
                        }
                    }
                }
            }
            return map;
        }

        public List<Combatant> GetRemaingCombatants()
        {
            return Combatants.Where(a => !a.Dead).OrderBy(a => a.Position.Y).ThenBy(a => a.Position.X).ToList();
        }

        public void DumpMap(Map2D<int> map, string filename, Coord h1 = null, List<Coord> h2 = null, Coord h3 = null, List<Coord> targets = null)
        {
            if (!DoDumpMap)
                return;

            string result = string.Empty;
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    Combatant combatant = Combatants.Where(c => c.Position.X == x && c.Position.Y == y && !c.Dead).SingleOrDefault();
                    Coord target = targets?.Where(t => t.X == x && t.Y == y).SingleOrDefault();
                    if (h1?.X == x && h1?.Y == y)
                    {
                        result += "H";
                    }
                    else
                    if (h3?.X == x && h3?.Y == y)
                    {
                        result += "T";
                    }
                    else
                    if (h2 != null && h2.Any(h => h.X == x && h.Y == y))
                    {
                        result += "·";
                    }
                    else
                    if (combatant != null)
                    {
                        result += combatant.Type;
                    }
                    else
                    if (target != null)
                    {
                        result += "$";
                    }
                    else

                    {
                        if (map[x, y] == int.MaxValue)
                        {
                            result += "#";
                        }

                        if (map[x, y] == 0)
                        {
                            result += " ";
                        }

                        if (map[x, y] > 0 && map[x, y] <= 9)
                        {
                            result += map[x, y];
                        }

                        if (map[x, y] < int.MaxValue && map[x, y] > 9)
                        {
                            result += 9;
                        }
                    }
                }
                result += "   ";
                foreach (Combatant c in Combatants.Where(a => !a.Dead).Where(a => a.Position.Y == y).OrderBy(a => a.Position.X))
                {
                    result += $"{c.Type} HP: {c.HP}  ";
                }

                result += "\n";
            }
            File.WriteAllText($"C:\\temp\\{filename}", result);
        }

        public void DumpMapLarge(Map2D<int> map, string filename, Coord h1 = null, List<Coord> h2 = null, Coord h3 = null, List<Coord> targets = null)
        {
            if (!DoDumpMap)
                return;

            string result = string.Empty;
            string line1 = string.Empty;
            string line2 = string.Empty;


            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    StringBuilder l1 = new StringBuilder("  ");
                    string l2 = "  ";

                    Combatant combatant = Combatants.Where(c => c.Position.X == x && c.Position.Y == y && !c.Dead).SingleOrDefault();
                    Coord target = targets?.Where(t => t.X == x && t.Y == y).SingleOrDefault();
                    if (h1?.X == x && h1?.Y == y)
                    {
                        l1[1] = 'H';
                    }

                    if (h3?.X == x && h3?.Y == y)
                    {
                        l1[1] = 'T';
                    }

                    if (h2 != null && h2.Any(h => h.X == x && h.Y == y))
                    {
                        l1[1] += '·';
                    }

                    if (combatant != null)
                    {
                        l1[0] = combatant.Type;
                    }

                    if (target != null)
                    {
                        l1[1] = '$';
                    }

                    if (combatant == null)
                    {
                        if (map[x, y] == int.MaxValue)
                        {
                            l1 = new StringBuilder("##");
                            l2 = "##";
                        }
                    }

                    if (map[x, y] == 0)
                    {
                        l1 = new StringBuilder("  ");
                        l2 = "  ";
                    }

                    if (map[x, y] > 0 && map[x, y] < int.MaxValue)
                    {
                        l2 = $"{map[x, y].ToString("00")}";
                    }

                    line1 += l1.ToString();
                    line2 += l2;
                }
                result += line1;
                result += "   ";

                foreach (Combatant c in Combatants.Where(a => !a.Dead).Where(a => a.Position.Y == y).OrderBy(a => a.Position.X))
                {
                    result += $"{c.Type} HP: {c.HP}  ";
                }

                result += "\n";
                result += line2;
                result += "\n";
                line1 = string.Empty;
                line2 = string.Empty;
            }
            File.WriteAllText($"C:\\temp\\{filename}", result);
        }

    }

    public class Coord : Vector2D
    {
        public int Distance { get; set; }  
    }
    public class Combatant
    {
        public Vector2D Position { get; set; }
        public int HP { get; set; }
        public char Type { get; set; }
        public bool Dead { get; set; }      
        public string ToString()
        {
            return $"{Type}: {Position.ToString()} - ({HP})";
        }
    }
}
