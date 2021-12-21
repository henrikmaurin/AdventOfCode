using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day15 : DayBase
    {
        private RiskLevel[,] _RiskLevel;
        private int sizeX;
        private int sizeY;
        private RiskLevelMap Map;

        private AStar AStar = new AStar();

        public int Problem1()
        {
            string[] instructions = input.GetDataCached(2021, 15).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Parse(instructions);

            _RiskLevel[0, 0].Lowest = 0;
            _RiskLevel[0, 0].Level = 0;

            return FindLowest(0, 0, 0);
        }

        public int Problem2()
        {
            string[] instructions = input.GetDataCached(2021, 15).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            ParseMega(instructions);

            return RunAStar();
        }

        public void Parse(string[] data)
        {
            sizeY = data.Length;
            sizeX = data[0].Length;

            Map = new RiskLevelMap();
            Map.SizeX = sizeX;
            Map.SizeY = sizeY;
            Map.RiskLevel = new int[sizeX, sizeY];

            _RiskLevel = new RiskLevel[sizeX, sizeY];


            for (int y = 0; y < sizeY; y++)
            {
                string line = data[y];
                for (int x = 0; x < sizeX; x++)
                {
                    char c = line[x];
                    _RiskLevel[x, y] = new RiskLevel();
                    _RiskLevel[x, y].Level = c.ToInt();
                    _RiskLevel[x, y].Lowest = int.MaxValue;
                    Map.RiskLevel[x, y] = c.ToInt();
                }
            }
        }

        public void ParseMega(string[] data)
        {
            sizeY = data.Length * 5;
            sizeX = data[0].Length * 5;

            Map = new RiskLevelMap();
            Map.SizeX = sizeX;
            Map.SizeY = sizeY;
            Map.RiskLevel = new int[sizeX, sizeY];

            _RiskLevel = new RiskLevel[sizeX, sizeY];


            for (int y = 0; y < sizeY / 5; y++)
            {
                string line = data[y];
                for (int x = 0; x < sizeX / 5; x++)
                {
                    char c = line[x];
                    for (int y1 = 0; y1 < 5; y1++)
                    {
                        for (int x1 = 0; x1 < 5; x1++)
                        {
                            int level = c.ToInt() + x1 + y1;
                            if (level > 9)
                                level -= 9;

                            _RiskLevel[x + x1 * sizeX / 5, y + y1 * sizeY / 5] = new RiskLevel();
                            _RiskLevel[x + x1 * sizeX / 5, y + y1 * sizeY / 5].Level = level;
                            _RiskLevel[x + x1 * sizeX / 5, y + y1 * sizeY / 5].Lowest = int.MaxValue;
                            Map.RiskLevel[x + x1 * sizeX / 5, y + y1 * sizeY / 5] = level;
                        }
                    }


                }
            }
        }

        public int FindLowest(int x, int y, int total)
        {
            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
                return int.MaxValue;

            if (total > (9 * 500 * 2))
                return int.MaxValue;

            if (total + _RiskLevel[x, y].Level >= _RiskLevel[x, y].Lowest)
                if (x != 0 || y != 0)
                    return int.MaxValue;

            total += _RiskLevel[x, y].Level;

            _RiskLevel[x, y].Lowest = total;

            if (x == sizeX - 1 && y == sizeY - 1)
                return _RiskLevel[x, y].Lowest;

            int result = FindLowest(x + 1, y, total);
            result = Lowest(result, FindLowest(x, y + 1, total));
            result = Lowest(result, FindLowest(x - 1, y, total));
            result = Lowest(result, FindLowest(x, y - 1, total));

            return result;
        }
  

        int Lowest(int x1, int x2)
        {
            if (x1 < x2)
                return x1;
            return x2;
        }

        public int RunAStar()
        {
            AStar.Init(0, 0, sizeX-1, sizeY-1);
            AStar.Map = Map;

            AStarNode goal = AStar.ProcessNext();
            while (goal == null)
            {
                goal = AStar.ProcessNext();
            }

            return goal.Cost;
        }
    }

    public class AStar
    {
        public List<AStarNode> ToProcess { get; set; }
        public List<AStarNode> Visited { get; set; }
        public RiskLevelMap Map { get; set; }
        private int _goalX;
        private int _goalY;

        public void Init(int startX, int startY, int goalX, int goalY)
        {
            _goalX = goalX;
            _goalY = goalY;

            ToProcess = new List<AStarNode>();
            Visited = new List<AStarNode>();

            ToProcess.Add(new AStarNode
            {
                Cost = 0,
                X = startX,
                Y = startY,
            });

        }

        public AStarNode ProcessNext()
        {
            int nextMin = ToProcess.Min(v => v.Cost);
            int indexToMin = ToProcess.FindIndex(v => v.Cost == nextMin);

            AStarNode current = ToProcess.ElementAt(indexToMin);
            ToProcess.RemoveAt(indexToMin);

            xy[] neighbors = { new xy { X = -1, Y = 0 }, new xy { X = 0, Y = -1 }, new xy { X = 1, Y = 0 }, new xy { X = 0, Y = 1 }, };

            int? level = null;

            //Left
            foreach (xy z in neighbors)
            {

                level = TryGetRiskLevel(current.X + z.X, current.Y + z.Y);
                if (level != null)
                {
                    AStarNode visited = TryGetVisitedAt(current.X + z.X, current.Y + z.Y);
                    if (visited != null)
                    {
                        if (visited.Cost > current.Cost + level.Value)
                        {
                            Visited.Remove(visited);
                            visited.CameFrom = current;
                            visited.Cost = current.Cost + level.Value;
                            ToProcess.AddOrUpdate(visited);
                        }
                    }
                    else
                    {
                        visited = new AStarNode
                        {
                            X = current.X + z.X,
                            Y = current.Y + z.Y,
                            CameFrom = current,
                            Cost = current.Cost + level.Value,
                        };
                        ToProcess.AddOrUpdate(visited);
                        if (visited.X == _goalX && visited.Y == _goalY)
                            return visited;
                    }
                }
            }
            Visited.Add(current);

            return null;
        }

        public int? TryGetRiskLevel(int x, int y)
        {
            if (x >= Map.SizeX || y >= Map.SizeY || x < 0 || y < 0)
                return null;

            return Map.RiskLevel[x, y];
        }



        public AStarNode TryGetVisitedAt(int x, int y)
        {
            return Visited.SingleOrDefault(v => v.X == x && v.Y == y);
        }

        class xy
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

    }

    public static class AStarNodeListAddOrUpdate
    {
        public static void AddOrUpdate(this List<AStarNode> list, AStarNode item)
        {
            var existingItem = list.Where(l=>l.X==item.X && l.Y==item.Y).SingleOrDefault();
            if (existingItem==null)
            {
                list.Add(item);
                return;
            }

            if (existingItem.Cost > item.Cost)
            {
                list.Remove(existingItem);
                existingItem.Cost = item.Cost;
                list.Add(existingItem);
            }

        }
    }


    public class AStarNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public AStarNode CameFrom { get; set; }
    }

    public class RiskLevel
    {
        public int Level { get; set; }
        public int Lowest { get; set; }
    }

    public class RiskLevelMap
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int[,] RiskLevel { get; set; }
    }
}
