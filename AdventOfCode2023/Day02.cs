using System.Collections.Specialized;

using Common;

namespace AdventOfCode2023
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        List<string> data;
        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {

            long result = 0;

//            data = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
//Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
//Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
//Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
//Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".SplitOnDoubleNewline();


            foreach (var item in data)
            {
                string game = item.Split(":").First();
                string rounds = item.Split(":").Last();

                int red = 0;
                int green = 0;
                int blue = 0;
                bool failed = false;
                foreach (var round in rounds.Trim().Split(";"))
                {


                    foreach (string cubes in round.Trim().Split(","))
                    {
                        int amount = cubes.Trim().Split(" ").First().ToInt();
                        string color = cubes.Trim().Split(" ").Last().Trim();

                        switch (color)
                        {
                            case "red":
                                if (amount > 12)
                                    failed = true;
                                break;
                            case "blue":
                                if (amount > 14)
                                    failed = true;
                                break;
                            case "green":
                                if (amount > 13)
                                    failed = true;
                                break;
                        }
                    }


                }
                if (failed)
                {
                    continue;
                }
                int id = game.Split(" ").Last().ToInt();
                result += id;

            }

            Console.WriteLine(result);

            result = 0;

            foreach (var item in data)
            {
                string game = item.Split(":").First();
                string rounds = item.Split(":").Last();

                int red = 0;
                int green = 0;
                int blue = 0;

                foreach (var round in rounds.Trim().Split(";"))
                {
                    foreach (string cubes in round.Trim().Split(","))
                    {
                        int amount = cubes.Trim().Split(" ").First().ToInt();
                        string color = cubes.Trim().Split(" ").Last().Trim();

                        switch (color)
                        {
                            case "red":
                                if (red < amount)
                                    red = amount;
                                break;
                            case "blue":
                                if (blue < amount) blue = amount;

                                break;
                            case "green":
                                if (green < amount) green = amount;

                                break;
                        }
                    }





                }
                int id = game.Split(" ").Last().ToInt();
                result += red * green * blue;

            }

            Console.WriteLine(result);








            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            return 0;
        }
        public int Problem2()
        {
            return 0;
        }
    }
}
