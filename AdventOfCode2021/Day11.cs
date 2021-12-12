using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day11 : DayBase
    {
        int[,] octopuses;
        int sizeX = 0;
        int sizeY = 0;
        int flashes = 0;
        public int Problem1()
        {
            string[] instructions = input.GetDataCached(2021, 11).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Init(instructions);


            return CountFlashes(100);
        }


        public Int64 Problem2()
        {
            string[] instructions = input.GetDataCached(2021, 11).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Init(instructions);


            return FindTotalFlash();
        }

        public void Init(string[] energyFieldData)
        {
            sizeY = energyFieldData.Length;
            sizeX = energyFieldData[0].Length;
            octopuses = new int[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    octopuses[y, x] = energyFieldData[y][x].ToInt();
        }

        public int FindTotalFlash()
        {
            int i = 0;
            flashes = 0;
            while (flashes != sizeX*sizeY)
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
                for (int y = 0; y < sizeY; y++)
                    for (int x = 0; x < sizeX; x++)                    
                        Increase(x, y);
                   
                ResetFlashed();
            }

            return flashes;
        }

        public void ResetFlashed()
        {

            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    if (octopuses[y, x] > 9)
                        octopuses[y, x] = 0;
        }

        public bool Increase(int x, int y)
        {
            if (x < 0 || y < 0 || y >= sizeY)
                return false;

            if (x >= sizeX)
                return false;

            if (octopuses[y, x] > 9)
                return false;

            octopuses[y, x]++;

            if (octopuses[y, x] > 9)
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
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    sb.Append(octopuses[y, x]);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
