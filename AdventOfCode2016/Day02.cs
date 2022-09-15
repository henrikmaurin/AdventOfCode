using System;
using System.IO;

namespace AdventOfCode2016
{
    public class Day02
    {
        private string[] datarows;

        public Day02(bool demodata = false)
        {
            if (!demodata)
                datarows = File.ReadAllText("data\\2.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            else
                datarows = File.ReadAllText("demodata\\2.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }


        public string Problem1()
        {
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
