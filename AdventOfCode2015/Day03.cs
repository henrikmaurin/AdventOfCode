using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day03
    {
        public int Problem1()
        {
            string data = ReadFile.ReadText("Day03.txt");

            return DeliverByInstructions(data);
        }

        public int Problem2()
        {
            string data = ReadFile.ReadText("Day03.txt");

            return DeliverByInstructionsWithRoboSanta(data);
        }

        public int DeliverByInstructions(string instructions)
        {
            Dictionary<string, int> visitedHouses = new Dictionary<string, int>();

            int x = 0;
            int y = 0;

            visitedHouses.Add($"{x}x{y}",1);

        
            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case '^':
                        y--;
                        break;
                    case 'v':
                        y++;
                        break;
                    case '<':
                        x--;
                        break;
                    case '>':
                        x++;
                        break;
                }
                if(visitedHouses.TryGetValue($"{x}x{y}",out int value))
                    {
                    visitedHouses[$"{x}x{y}"] = value + 1;
                }
                else
                    visitedHouses.Add($"{x}x{y}", 1);

            }

            return visitedHouses.Count();
        }

        public int DeliverByInstructionsWithRoboSanta(string instructions)
        {
            Dictionary<string, int> visitedHouses = new Dictionary<string, int>();

            int[] x = {0,0};
            int[] y = {0,0};           

            visitedHouses.Add($"0x0", 2);

            int who = 0;


            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case '^':
                        y[who]--;
                        break;
                    case 'v':
                        y[who]++;
                        break;
                    case '<':
                        x[who]--;
                        break;
                    case '>':
                        x[who]++;
                        break;
                }
                if (visitedHouses.TryGetValue($"{x[who]}x{y[who]}", out int value))
                {
                    visitedHouses[$"{x[who]}x{y[who]}"] = value + 1;
                }
                else
                    visitedHouses.Add($"{x[who]}x{y[who]}", 1);

                who = (who + 1) % 2;

            }

            return visitedHouses.Count();
        }
    }
}
