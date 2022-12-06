using Common;
using System.Text;

namespace AdventOfCode2021
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        private string[] instructions;

        private Map2D<int> Octopuses;

        /*int[,] octopuses;
        int sizeX = 0;
        int sizeY = 0;*/
        int flashes = 0;

        public Day11(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            instructions = input.GetDataCached().SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Total flashes: {result1}");

            Int64 result2 = Problem2();
            Console.WriteLine($"P2: Step: {result2}");
        }

        public int Problem1()
        {
            Init(instructions);

            return CountFlashes(100);
        }


        public Int64 Problem2()
        {
            Init(instructions);

            return FindTotalFlash();
        }

        public void Init(string[] energyFieldData)
        {
            Octopuses = new Map2D<int>();
            Octopuses.SafeOperations = true;
            Octopuses.Init(energyFieldData[0].Length, energyFieldData.Length);
            for (int y = 0; y < Octopuses.SizeY; y++)
                for (int x = 0; x < Octopuses.SizeX; x++)
                    Octopuses[x, y] = energyFieldData[y][x].ToInt();

          /*  sizeY = energyFieldData.Length;
            sizeX = energyFieldData[0].Length;
            octopuses = new int[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    octopuses[y, x] = energyFieldData[y][x].ToInt();*/
        }

        public int FindTotalFlash()
        {
            int i = 0;
            flashes = 0;
            while (flashes != Octopuses.SizeX * Octopuses.SizeY)
            {
                i++;
                flashes = 0;
                CountFlashes(1);
            }

            return i;
        }

        public int CountFlashes(int cycles)
        {
            for (int i = 0; i < cycles; i++)
            {
                for (int y = 0; y < Octopuses.SizeY; y++)
                    for (int x = 0; x < Octopuses.SizeX; x++)
                        Increase(x, y);

                ResetFlashed();
            }

            return flashes;
        }

        public void ResetFlashed()
        {

            for (int y = 0; y < Octopuses.SizeY; y++)
                for (int x = 0; x < Octopuses.SizeX; x++)
                    if (Octopuses[x, y] > 9)
                        Octopuses[x, y] = 0;
        }

        public bool Increase(int x, int y)
        {
            if (x < 0 || y < 0 || y >= Octopuses.SizeY)
                return false;

            if (x >= Octopuses.SizeX)
                return false;

            if (Octopuses[x, y] > 9)
                return false;

            Octopuses[x, y]++;

            if (Octopuses[x, y] > 9)
            {
                flashes++;
                IncreaseAdjacant(x, y);

            }
            return true;
        }

        public bool IncreaseAdjacant(int aroundX, int aroundY)
        {
            for (int y = -1; y <= 1; y++)
                for (int x = -1; x <= 1; x++)
                    if (y != 0 || x != 0 || x != y)
                        Increase(aroundX + x, aroundY + y);
            return true;
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Octopuses.SizeY; y++)
            {
                for (int x = 0; x < Octopuses.SizeX; x++)
                {
                    sb.Append(Octopuses[x, y]);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
