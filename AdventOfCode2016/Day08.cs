using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day08 : DayBase, IDay
    {
        private List<string> lines;
        int[] display;
        int dimX, dimY;

        public Day08() : base(2016, 8) { lines = input.GetDataCached().SplitOnNewline(true); }

        public int Problem1()
        {
            dimX = 50;
            dimY = 6;

            display = new int[dimX * dimY];

            foreach (string instruction in lines)
            {
                if (instruction.StartsWith("rect"))
                {
                    int[] param = instruction.Substring(5).Split('x').Select(i => int.Parse(i)).ToArray();
                    Rect(param[0], param[1]);
                }
                if (instruction.StartsWith("rotate row"))
                {
                    int[] param = instruction.Substring(13).Split(" by ").Select(i => int.Parse(i)).ToArray();
                    RotateRow(param[0], param[1]);
                }
                if (instruction.StartsWith("rotate column"))
                {
                    int[] param = instruction.Substring(16).Split(" by ").Select(i => int.Parse(i)).ToArray();
                    RotateColumn(param[0], param[1]);
                }

            }

            return display.Sum();
        }

        private void Rect(int x, int y)
        {
            for (int yp = 0; yp < y; yp++)
                for (int xp = 0; xp < x; xp++)
                {
                    display[xp + yp * dimX] = 1;
                }
        }

        private void RotateColumn(int col, int by)
        {
            for (int i = 0; i < by; i++)
            {
                int mem = display[col + (dimY - 1) * dimX];
                for (int y = dimY - 2; y >= 0; y--)
                {
                    display[col + (y + 1) * dimX] = display[col + y * dimX];
                }
                display[col] = mem;
            }
        }

        private void RotateRow(int row, int by)
        {
            for (int i = 0; i < by; i++)
            {
                int mem = display[dimX - 1 + row * dimX];
                for (int x = dimX - 2; x >= 0; x--)
                {
                    display[row * dimX + x + 1] = display[row * dimX + x];
                }
                display[row * dimX] = mem;
            }
        }

        public void Display()
        {
            for (int yp = 0; yp < dimY; yp++)
            {
                for (int xp = 0; xp < dimX; xp++)
                {
                    Console.Write(display[xp + yp * dimX] == 1 ? '*' : ' ');
                }
                Console.WriteLine();
            }
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            Display();
        }
    }
}
