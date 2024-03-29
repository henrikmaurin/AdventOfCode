﻿using Common;

namespace AdventOfCode2015
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        private int[,] grid = new int[1000, 1000];
        private bool useNewInstructions = false;

        public Day06(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;
        }

        public int Problem1()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);
            foreach (string line in data)
                Parse(line);

            return SetCount();
        }

        public int Problem2()
        {
            UseNewInstructions();
            UnSetAll();
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);
            foreach (string line in data)
                Parse(line);

            return SetCount();
        }

        public void Run()
        {
            int lightsLit = Problem1();
            Console.WriteLine($"P1: Lights lit: {lightsLit}");

            int lightsLitNewInstructions = Problem2();
            Console.WriteLine($"P2: Santa ends up in basemant at position: {lightsLitNewInstructions}");
        }

        public void UseNewInstructions()
        {
            useNewInstructions = true;
        }


        public int SetCount()
        {
            return grid.Cast<int>().Sum();
        }

        public void UnSetAll()
        {

            UnSet(0, 0, 999, 999);
        }

        public void SetAll()
        {
            Set(0, 0, 999, 999);
        }

        public void Set(int fromX, int fromY, int toX, int ToY)
        {
            for (int x = fromX; x <= toX; x++)
                for (int y = fromY; y <= ToY; y++)
                    Set(x, y);
        }
        public void UnSet(int fromX, int fromY, int toX, int ToY)
        {
            for (int x = fromX; x <= toX; x++)
                for (int y = fromY; y <= ToY; y++)
                    UnSet(x, y);
        }
        public void Toggle(int fromX, int fromY, int toX, int ToY)
        {
            for (int x = fromX; x <= toX; x++)
                for (int y = fromY; y <= ToY; y++)
                    Toggle(x, y);
        }

        public void Parse(string line)
        {
            Operation operation = Operation.Undefined;

            if (line.StartsWith("turn on"))
            {
                operation = Operation.TurnOn;
                line = line.Substring(8);
            }
            else if (line.StartsWith("turn off"))
            {
                operation = Operation.TurnOff;
                line = line.Substring(9);
            }
            else if (line.StartsWith("toggle"))
            {
                operation = Operation.Toggle;
                line = line.Substring(7);
            }

            int[] coords = line.Replace(" through ", ",").Split(',').Select(l => int.Parse(l)).ToArray();

            switch (operation)
            {
                case Operation.TurnOn:
                    Set(coords[0], coords[1], coords[2], coords[3]);
                    break;
                case Operation.TurnOff:
                    UnSet(coords[0], coords[1], coords[2], coords[3]);
                    break;
                case Operation.Toggle:
                    Toggle(coords[0], coords[1], coords[2], coords[3]);
                    break;
            }
        }

        public void Set(int x, int y)
        {
            if (useNewInstructions)
                grid[x, y]++;
            else
                grid[x, y] = 1;
        }

        public void UnSet(int x, int y)
        {
            grid[x, y]--;
            if (grid[x, y] < 0)
                grid[x, y] = 0;
        }


        public void Toggle(int x, int y)
        {
            if (useNewInstructions)
                grid[x, y] += 2;
            else
                grid[x, y] = 1 - grid[x, y];
        }

        enum Operation
        {
            Undefined,
            TurnOn,
            TurnOff,
            Toggle
        }

    }
}
