using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day15 : DayBase, IDay
    {
        public int[,] Map { get; set; }
        public int[,] WorkingMap { get; set; }
        public List<Combatant> Combatants { get; set; }
        public int xSize { get; private set; }
        public int ySize { get; private set; }
        public List<Coord> targets { get; private set; }
        public string[] Data { get; set; }

        public Day15() : base(2018, 15)
        {
            Data = input.GetDataCached().SplitOnNewlineArray();

        }

        private void InitMap()
        {
            xSize = Data[0].Length;
            ySize = Data.Length;

            Combatants = new List<Combatant>();

            Map = new int[xSize, ySize];

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    switch (Data[y][x])
                    {
                        case '#':
                            Map[x, y] = int.MaxValue;
                            break;
                        case 'E':
                        case 'G':
                            Combatant p = new Combatant { X = x, Y = y, Type = Data[y][x], HP = 200, Dead = false };
                            Map[x, y] = 0;
                            Combatants.Add(p);
                            break;
                    }
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

            while (!done)
            {
                List<Combatant> remaining = Combatants.Where(a => !a.Dead).OrderBy(a => a.Y).ThenBy(a => a.X).ToList();

                foreach (Combatant c in remaining)
                {
                    //                    DumpMap(Map, "Anim.txt");
                    //                    Thread.Sleep(5);

                    if (!c.Dead)
                    {
                        targets = FindTargets(c);

                        ClumsyMap(c.X, c.Y);
                        DumpMapLarge(WorkingMap, "Working.txt", new Coord { X = c.X, Y = c.Y });

                        //                        DumpMap(WorkingMap, "Anim.txt");
                        //                     Thread.Sleep(5);
                        int minDistance = int.MaxValue;

                        foreach (Coord target in targets)
                        {

                            if (WorkingMap[target.X, target.Y] > 0 && WorkingMap[target.X, target.Y] < int.MaxValue)
                            {
                                target.Distance = WorkingMap[target.X, target.Y];
                                target.InReach = true;
                                if (target.Distance <= minDistance)
                                {
                                    minDistance = target.Distance;
                                    Coord backtrackDir;
                                    if (WorkingMap[target.X, target.Y - 1] == minDistance - 1)
                                    {
                                        backtrackDir = Backtrack(target.X, target.Y - 1).Last();
                                    }
                                    else if (WorkingMap[target.X - 1, target.Y] == minDistance - 1)
                                    {
                                        backtrackDir = Backtrack(target.X - 1, target.Y).Last();
                                    }
                                    else if (WorkingMap[target.X + 1, target.Y] == minDistance - 1)
                                    {
                                        backtrackDir = Backtrack(target.X + 1, target.Y).Last();
                                    }
                                    else if (WorkingMap[target.X, target.Y + 1] == minDistance - 1)
                                    {
                                        backtrackDir = Backtrack(target.X, target.Y + 1).Last();
                                    }
                                    else
                                    {
                                        //target.Distance = int.MaxValue;
                                        backtrackDir = new Coord();
                                    }




                                    if (backtrackDir.Y < c.Y)
                                        target.TravelRank = 0;
                                    else if (backtrackDir.Y > c.Y)
                                        target.TravelRank = 3;
                                    else
                                    {
                                        if (backtrackDir.X < c.X)
                                            target.TravelRank = 1;
                                        else
                                            target.TravelRank = 2;
                                    }
                                }
                                else
                                    target.TravelRank = 4;


                            }
                            if (target.X == c.X && target.Y == c.Y)
                            {
                                target.Distance = 0;
                                target.InReach = true;
                            }

                        }
                        Coord ChosenTarget = targets?.Where(t => t.InReach == true).OrderBy(t => t.Distance).ThenBy(t => t.TravelRank).FirstOrDefault();
                        if (rounds == 23 && c.Y == 15)
                        {
                            //DumpMap(Map, "FoundPath.txt", new Coord { X = c.X, Y = c.Y }, Backtrack(ChosenTarget.X, ChosenTarget.Y), new Coord { X = ChosenTarget.X, Y = ChosenTarget.Y });
                        }


                        if (ChosenTarget != null && !(ChosenTarget.X == c.X && ChosenTarget.Y == c.Y))
                        {
                            List<Coord> nextMove = Backtrack(ChosenTarget.X, ChosenTarget.Y);
                            //Console.WriteLine($"{c.Type} moves from {c.X},{c.Y} to {nextMove.Last().X},{nextMove.Last().Y}");
                            //DumpMap(Map, "Debug.txt", new Coord { X = c.X, Y = c.Y }, nextMove, ChosenTarget);
                            //DumpMap(WorkingMap, "WorkingMap.txt");
                            c.X = nextMove.Last().X;
                            c.Y = nextMove.Last().Y;
                            //Console.ReadKey();
                            //DumpMap(Map, "Debug.txt");
                        }
                        Attack(c);



                        if (!Combatants.Any(e => e.Type != c.Type && !e.Dead))
                        {

                            done = true;
                            int remainingHP = Combatants.Sum(co => co.HP);
                            if (c == remaining.Where(a => !a.Dead).Last())
                            {
                                rounds++;
                            }
                            DumpMap(Map, "End.txt");

                            return $"Done after {rounds} rounds, Remaining HP: {remainingHP}, Answer: {rounds * remainingHP} ";

                        }
                    }
                    //DumpMap(Map, $"Round1.txt");
                    ///Console.ReadKey();
                }
                targets.Clear();
                DumpMap(Map, $"Round{rounds}.txt");
                //Console.ReadKey();
                rounds++;
            }
            return string.Empty;
        }


        public int Problem2(int ElfPower = 3)
        {
            InitMap();
            bool done = false;
            int rounds = 0;
            int elfCount = Combatants.Where(e => e.Type == 'E').Count();
            while (!done)
            {
                Console.WriteLine($"Round {rounds}");
                List<Combatant> remaining = Combatants.Where(a => !a.Dead).OrderBy(a => a.Y).ThenBy(a => a.X).ToList();
                if (rounds == 126)
                {
                    rounds = 126;
                }

                foreach (Combatant c in remaining)
                {
                    if (!c.Dead)
                    {
                        targets = FindTargets(c);


                        ClumsyMap(c.X, c.Y);
                        //                       DumpMap(WorkingMap, "state");
                        foreach (Coord target in targets)
                        {
                            if (WorkingMap[target.X, target.Y] > 0 && WorkingMap[target.X, target.Y] < int.MaxValue)
                            {
                                target.Distance = WorkingMap[target.X, target.Y];
                                target.InReach = true;
                            }
                            if (target.X == c.X && target.Y == c.Y)
                            {
                                target.Distance = 0;
                                target.InReach = true;
                            }

                        }
                        Coord ChosenTarget = targets?.Where(t => t.InReach == true).OrderBy(t => t.Distance).ThenBy(t => t.Y).ThenBy(t => t.X).FirstOrDefault();
                        if (ChosenTarget != null && !(ChosenTarget.X == c.X && ChosenTarget.Y == c.Y))
                        {
                            List<Coord> nextMove = Backtrack(ChosenTarget.X, ChosenTarget.Y);
                            //                            Console.WriteLine($"{c.Type} moves from {c.X},{c.Y} to {nextMove.Last().X},{nextMove.Last().Y}");
                            //                            DumpMap(Map, "Debug.txt", new Coord { X = c.X, Y = c.Y }, nextMove, ChosenTarget);
                            //                            DumpMap(WorkingMap, "WorkingMap.txt");
                            c.X = nextMove.Last().X;
                            c.Y = nextMove.Last().Y;
                            //Console.ReadKey();
                            //DumpMap(Map, "Debug.txt");
                        }
                        Attack(c, ElfPower);



                        if (!Combatants.Any(e => e.Type != c.Type && !e.Dead))
                        {

                            done = true;
                            int remainingHP = Combatants.Sum(co => co.HP);
                            if (c == remaining.Where(a => !a.Dead).Last())
                            {
                                rounds++;
                            }

                            Console.WriteLine($"Done after {rounds} rounds, Remaining HP: {remainingHP}, Answer: {rounds * remainingHP} ");
                            //                           DumpMap(Map, "End.txt");
                            int alive = Combatants.Where(e => e.Type == 'E' && !e.Dead).Count();
                            return elfCount - alive;
                        }
                    }
                    //DumpMap(Map, $"Round1.txt");
                    ///Console.ReadKey();
                }
                targets.Clear();
                //DumpMap(Map, $"Round1.txt");
                //Console.ReadKey();
                rounds++;
            }
            return int.MaxValue;
        }

        public List<Coord> FindTargets(Combatant combatant)
        {
            targets = new List<Coord>();
            foreach (Combatant enemy in Combatants.Where(e => e.Type != combatant.Type && !e.Dead && e != combatant))
            {
                if (Map[enemy.X - 1, enemy.Y] == 0)
                {
                    if (!targets.Any(t => t.X == enemy.X - 1 && t.Y == enemy.Y))
                    {
                        targets.Add(new Coord { X = enemy.X - 1, Y = enemy.Y });
                    }
                }

                if (Map[enemy.X + 1, enemy.Y] == 0)
                {
                    if (!targets.Any(t => t.X == enemy.X + 1 && t.Y == enemy.Y))
                    {
                        targets.Add(new Coord { X = enemy.X + 1, Y = enemy.Y });
                    }
                }

                if (Map[enemy.X, enemy.Y - 1] == 0)
                {
                    if (!targets.Any(t => t.X == enemy.X && t.Y == enemy.Y - 1))
                    {
                        targets.Add(new Coord { X = enemy.X, Y = enemy.Y - 1 });
                    }
                }

                if (Map[enemy.X, enemy.Y + 1] == 0)
                {
                    if (!targets.Any(t => t.X == enemy.X && t.Y == enemy.Y + 1))
                    {
                        targets.Add(new Coord { X = enemy.X, Y = enemy.Y + 1 });
                    }
                }
            }
            return targets;
        }


        public void Attack(Combatant combatant, int elfPower = 3)
        {
            Combatant enemy = Combatants.Where(c => !c.Dead && c.Type != combatant.Type)
                  .Where(c => c.X == combatant.X && c.Y == combatant.Y - 1 ||
                       c.X == combatant.X && c.Y == combatant.Y + 1 ||
                       c.X == combatant.X - 1 && c.Y == combatant.Y ||
                       c.X == combatant.X + 1 && c.Y == combatant.Y)
                    .OrderBy(c => c.HP)
                    .ThenBy(c => c.Y)
                    .ThenBy(c => c.X)
                    .FirstOrDefault();

            if (enemy != null)
            {
                if ((enemy.X == combatant.X && enemy.Y == combatant.Y - 1)
                    || (enemy.X == combatant.X && enemy.Y == combatant.Y + 1)
                    || (enemy.X == combatant.X - 1 && enemy.Y == combatant.Y)
                    || (enemy.X == combatant.X + 1 && enemy.Y == combatant.Y)
                    )
                {
                    if (enemy.Type == 'G')
                    {
                        enemy.HP -= elfPower;
                    }
                    else
                    {
                        enemy.HP -= 3;
                    }

                    //                   Console.WriteLine($"{combatant.Type} at {combatant.X},{combatant.Y} attacks {enemy.Type} at  {enemy.X},{enemy.Y}");
                    if (enemy.HP <= 0)
                    {
                        //Console.WriteLine($"{enemy.Type} at {enemy.X},{enemy.Y} dies");
                        enemy.Dead = true;
                        enemy.HP = 0;

                    }
                }
            }
        }

        public List<Coord> Backtrack(int x, int y)
        {
            List<Coord> track = new List<Coord>();
            track.Add(new Coord { X = x, Y = y });
            int currentVal = WorkingMap[x, y];
            while (currentVal > 1)
            {
                if (WorkingMap[x, y + 1] == currentVal - 1)
                {
                    y--;
                }
                else if (WorkingMap[x + 1, y] == currentVal - 1)
                {
                    x--;
                }
                else if (WorkingMap[x - 1, y] == currentVal - 1)
                {
                    x++;
                }
                else if (WorkingMap[x, y - 1] == currentVal - 1)
                {
                    y++;
                }
                track.Add(new Coord { X = x, Y = y });
                currentVal--;
            }
            return track;
        }

        public void ClumsyMap(int xpos, int ypos)
        {
            int changes = 1;
            WorkingMap = new int[xSize, ySize];


            foreach (Combatant c in Combatants.Where(c => !c.Dead))
            {
                WorkingMap[c.X, c.Y] = int.MaxValue;
            }

            if (WorkingMap[xpos - 1, ypos] == 0 && Map[xpos - 1, ypos] != int.MaxValue)
            {
                WorkingMap[xpos - 1, ypos] = 1;
            }

            if (WorkingMap[xpos + 1, ypos] == 0 && Map[xpos + 1, ypos] != int.MaxValue)
            {
                WorkingMap[xpos + 1, ypos] = 1;
            }

            if (WorkingMap[xpos, ypos - 1] == 0 && Map[xpos, ypos - 1] != int.MaxValue)
            {
                WorkingMap[xpos, ypos - 1] = 1;
            }

            if (WorkingMap[xpos, ypos + 1] == 0 && Map[xpos, ypos + 1] != int.MaxValue)
            {
                WorkingMap[xpos, ypos + 1] = 1;
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
                            WorkingMap[x, y] = int.MaxValue;
                        }
                        else if (WorkingMap[x, y] == range)
                        {
                            if (WorkingMap[x - 1, y] == 0)
                            {
                                WorkingMap[x - 1, y] = range + 1;
                                changes++;
                            }

                            if (WorkingMap[x + 1, y] == 0)
                            {
                                WorkingMap[x + 1, y] = range + 1;
                                changes++;
                            }

                            if (WorkingMap[x, y - 1] == 0)
                            {
                                WorkingMap[x, y - 1] = range + 1;
                                changes++;
                            }

                            if (WorkingMap[x, y + 1] == 0)
                            {
                                WorkingMap[x, y + 1] = range + 1;
                                changes++;
                            }
                        }
                    }
                }
            }

        }

        public void DumpMap(int[,] map, string filename, Coord h1 = null, List<Coord> h2 = null, Coord h3 = null)
        {
            string result = string.Empty;
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    Combatant combatant = Combatants.Where(c => c.X == x && c.Y == y && !c.Dead).SingleOrDefault();
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
                foreach (Combatant c in Combatants.Where(a => !a.Dead).Where(a => a.Y == y).OrderBy(a => a.X))
                {
                    result += $"{c.Type} HP: {c.HP}  ";
                }

                result += "\n";
            }
            File.WriteAllText($"C:\\temp\\{filename}", result);
        }

        public void DumpMapLarge(int[,] map, string filename, Coord h1 = null, List<Coord> h2 = null, Coord h3 = null)
        {
            string result = string.Empty;
            string line1 = string.Empty;
            string line2 = string.Empty;


            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    StringBuilder l1 = new StringBuilder("  ");
                    string l2 = "  ";

                    Combatant combatant = Combatants.Where(c => c.X == x && c.Y == y && !c.Dead).SingleOrDefault();
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

                foreach (Combatant c in Combatants.Where(a => !a.Dead).Where(a => a.Y == y).OrderBy(a => a.X))
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

    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }
        public bool InReach { get; set; }
        public int TravelRank { get; set; }
    }
    public class Combatant
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int HP { get; set; }
        public char Type { get; set; }
        public bool Dead { get; set; }
        //public bool Dieing { get; set; }
    }
}
