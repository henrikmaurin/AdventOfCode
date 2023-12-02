using Common;

using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        string data;
        private IEnumerable<Game> setOfGames;
        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached();
            setOfGames = Parser.ParseLinesDelimitedByNewline<Game, Game.Parsed>(data);
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: The sum of all possible games is {Answer(result1)}");

            int result2 = Problem2();
            Console.WriteLine($"P2: The sum of power is {Answer(result2)}");
        }
        public int Problem1()
        {
            return GameElf.PlayGamesWithLimits(setOfGames);
        }

        public int Problem2()
        {
            return GameElf.FindTotalPower(setOfGames);
        }

        public static class GameElf
        {
            public static int PlayGamesWithLimits(IEnumerable<Game> games)
            {
                int sum = 0;

                foreach (Game game in games)
                {
                    if (game.Draws.Select(d => d.Red).Where(r => r > 12).Any())
                        continue;
                    if (game.Draws.Select(d => d.Green).Where(r => r > 13).Any())
                        continue;
                    if (game.Draws.Select(d => d.Blue).Where(r => r > 14).Any())
                        continue;

                    sum += game.GameNo;
                }
                return sum;
            }

            public static int FindTotalPower(IEnumerable<Game> games)
            {
                int sum = 0;

                foreach (Game game in games)
                {
                    int product = 1;

                    product *= game.Draws.Select(d => d.Red).Max();
                    product *= game.Draws.Select(d => d.Green).Max();
                    product *= game.Draws.Select(d => d.Blue).Max();

                    sum += product;
                }
                return sum;

            }
        }

            public class Game : IParsedDataFormat
            {
                public int GameNo { get; set; }
                public List<Draw> Draws { get; set; }

                public class Parsed : IInDataFormat
                {
                    public string DataFormat => @"Game (\d+): (.+)";

                    public string[] PropertyNames => new string[] { nameof(GameNo), nameof(Draws) };

                    public int GameNo { get; set; }
                    public string Draws { get; set; }
                }

                public void Transform(IInDataFormat data)
                {
                    Parsed gameRound = (Parsed)data;
                    GameNo = gameRound.GameNo;
                    Draws = new List<Draw>();

                    foreach (string draw in gameRound.Draws.Split(';'))
                    {
                        Draw d = Draw.FromText(draw.Trim());
                        Draws.Add(d);
                    }
                }
            }

            public class Draw
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }

                public static Draw FromText(string text)
                {
                    Draw draw = new Draw();

                    List<string> cubes = text.Split(',').ToList();
                    foreach (string amountAndColor in cubes)
                    {
                        int amount = amountAndColor.Trim().Split(" ").First().ToInt();
                        switch (amountAndColor.Trim().Split(" ").Last().Trim())
                        {
                            case "red":
                                draw.Red += amount;
                                break;
                            case "green":
                                draw.Green += amount;
                                break;
                            case "blue":
                                draw.Blue += amount;
                                break;
                        }
                    }
                    return draw;
                }

                IEnumerable<Cubes> GetCubes()
                {
                    List<Cubes> cubes = new List<Cubes>();
                    if (Red > 0)
                    {
                        cubes.Add(new Cubes { Color = ColorEnum.Red, Amount = Red });
                    }
                    if (Green > 0)
                    {
                        cubes.Add(new Cubes { Color = ColorEnum.Green, Amount = Green });
                    }
                    if (Blue > 0)
                    {
                        cubes.Add(new Cubes { Color = ColorEnum.Blue, Amount = Blue });
                    }
                    return cubes;
                }

            }

            public class Cubes
            {
                public int Amount { get; set; }
                public ColorEnum Color { get; set; }
            }

            public enum ColorEnum
            {
                Red,
                Green,
                Blue,
            }
        }
    }
