using Common;
using System.Text;

namespace AdventOfCode2021
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        private string[] data;
        private List<DrawInstructions> _drawInstructions;
        public Day05(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;
            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of points: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of points: {result2}");
        }
        public void Init()
        {
            _drawInstructions = new List<DrawInstructions>();
        }

        public int Problem1()
        {
            Init();
            foreach (string line in data.Where(d => !string.IsNullOrEmpty(d)))
                _drawInstructions.Add(Parse(line));

            Map map = new Map(_drawInstructions.Select(d => d.ToX).Max() + 1, _drawInstructions.Select(d => d.ToY).Max() + 1);

            foreach (DrawInstructions drawInstruction in _drawInstructions)
                map.Draw(drawInstruction.FromX, drawInstruction.FromY, drawInstruction.ToX, drawInstruction.ToY);

            return map.GetDangerZones();
        }

        public int Problem2()
        {
            Init();
            foreach (string line in data.Where(d => !string.IsNullOrEmpty(d)))
                _drawInstructions.Add(Parse(line));

            Map map = new Map(_drawInstructions.Select(d => d.ToX).Max() + 1, _drawInstructions.Select(d => d.ToY).Max() + 1);

            foreach (DrawInstructions drawInstruction in _drawInstructions)
                map.Draw(drawInstruction.FromX, drawInstruction.FromY, drawInstruction.ToX, drawInstruction.ToY, true);

            return map.GetDangerZones();
        }


        public DrawInstructions Parse(string instruction)
        {
            int[] vals = instruction.Replace(" -> ", ",").Split(',').ToInt();

            DrawInstructions drawInstructions = new DrawInstructions
            {
                FromX = vals[0],
                FromY = vals[1],
                ToX = vals[2],
                ToY = vals[3],
            };

            return drawInstructions;
        }

        public class DrawInstructions
        {
            public int FromX { get; set; }
            public int FromY { get; set; }
            public int ToX { get; set; }
            public int ToY { get; set; }
        }
    }


    public class Map
    {
        private int[] _map;
        private int _xSize;
        private int _ySize;

        public Map(int xSize, int ySize)
        {
            _xSize = xSize;
            _ySize = ySize;
            _map = new int[_xSize * _ySize];
        }

        public void Draw(int fromX, int fromY, int toX, int toY, bool allowDiagonal = false)
        {
            if (_map == null)
                return;

            if (!allowDiagonal && fromX != toX && fromY != toY)
                return;

            int x = fromX;
            int y = fromY;
            int stepX = Math.Sign(toX - fromX);
            int stepY = Math.Sign(toY - fromY);

            while (IsBetween(x, fromX, toX) && IsBetween(y, fromY, toY))
            {
                _map[y * _xSize + x]++;
                x += stepX;
                y += stepY;
            }
        }

        public bool IsBetween(int n, int n1, int n2)
        {
            if (n1 < n2)
                return (n >= n1 && n <= n2);
            return (n >= n2 && n <= n1);


        }

        public int GetDangerZones()
        {
            return _map.Where(m => m > 1).Count();
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                    sb.Append(_map[y * _xSize + x]);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
