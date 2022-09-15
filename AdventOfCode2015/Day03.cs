using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day03 : DayBase, IDay
    {
        public Day03() : base(2015, 3) { }

        public int Problem1()
        {
            string data = input.GetDataCached();

            return DeliverByInstructions(data);
        }

        public int Problem2()
        {
            string data = input.GetDataCached();

            return DeliverByInstructionsWithRoboSanta(data);
        }

        public void Run()
        {
            int visitedHouses = Problem1();
            Console.WriteLine($"Number of houses visited at least once by Santa: {visitedHouses}");

            visitedHouses = Problem2();
            Console.WriteLine($"Number of houses visited at least once by Santa and Robo-Santa: {visitedHouses}");

        }

        public int DeliverByInstructions(string instructions)
        {
            Dictionary<string, int> visitedHouses = new Dictionary<string, int>();

            int x = 0;
            int y = 0;

            visitedHouses.Add($"{x}x{y}", 1);


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
                if (visitedHouses.TryGetValue($"{x}x{y}", out int value))
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

            int[] x = { 0, 0 };
            int[] y = { 0, 0 };

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
