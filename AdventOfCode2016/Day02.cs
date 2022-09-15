using AdventOfCode;
using Common;
using System;

namespace AdventOfCode2016
{
    public class Day02 : DayBase, IDay
    {
        private string[] datarows;

        public Day02() : base(2016, 2) { }

        public void Run()
        {
            string bathroomCode = Problem1();
            Console.WriteLine($"P1: Code to bathroom: {bathroomCode}");

            bathroomCode = Problem2();
            Console.WriteLine($"P1: New code to bathroom: {bathroomCode}");
        }

        public string Problem1()
        {
            datarows = input.GetDataCached().SplitOnNewlineArray(true);

            string code = string.Empty;
            int posX = 1, posY = 1;

            foreach (string row in datarows)
            {
                foreach (char instruction in row.ToCharArray())
                {
                    switch (instruction)
                    {
                        case 'U': posY--; break;
                        case 'D': posY++; break;
                        case 'L': posX--; break;
                        case 'R': posX++; break;
                    }
                    if (posX < 0)
                        posX = 0;
                    if (posX > 2)
                        posX = 2;
                    if (posY < 0)
                        posY = 0;
                    if (posY > 2)
                        posY = 2;
                }
                int digit = posX + posY * 3 + 1;

                code += digit;
            }


            return code;
        }

        public string Problem2()
        {
            datarows = input.GetDataCached().SplitOnNewlineArray(true);

            string code = string.Empty;
            int posX = 1, posY = 3;

            char[,] keypad = new char[,] {
                {'0','0','0','0','0','0','0',},
                {'0','0','0','1','0','0','0',},
                {'0','0','2','3','4','0','0',},
                {'0','5','6','7','8','9','0',},
                {'0','0','A','B','C','0','0',},
                {'0','0','0','D','0','0','0',},
                {'0','0','0','0','0','0','0',}
            };

            foreach (string row in datarows)
            {
                foreach (char instruction in row.ToCharArray())
                {
                    char currentDigit = keypad[posY, posX];


                    switch (instruction)
                    {
                        case 'U':
                            if (keypad[posY - 1, posX] != '0')
                                posY--;
                            break;
                        case 'D':
                            if (keypad[posY + 1, posX] != '0')
                                posY++;
                            break;
                        case 'L':
                            if (keypad[posY, posX - 1] != '0')
                                posX--;
                            break;
                        case 'R':
                            if (keypad[posY, posX + 1] != '0')
                                posX++;
                            break;
                    }

                }
                char digit = keypad[posY, posX];

                code += digit;
            }


            return code;
        }
    }
}
