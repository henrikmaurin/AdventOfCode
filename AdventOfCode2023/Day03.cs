using Common;

namespace AdventOfCode2023
{

    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        List<string> data;
        private GondolaEngine gondolaEngine;
        public Day03(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            gondolaEngine = new GondolaEngine();
            gondolaEngine.ReadSchematics(data);
            gondolaEngine.Annotate();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: The sum of all components is {Answer(result1)}");

            int result2 = Problem2();
            Console.WriteLine($"P2: The sum of all gear ratios is {Answer(result2)}");
        }
        public int Problem1()
        {
            return SchematicsInterpreter.FindSum(gondolaEngine);
        }
        public int Problem2()
        {
            return SchematicsInterpreter.FindGearRatio(gondolaEngine);
        }

        public static class SchematicsInterpreter
        {
            public static int FindSum(GondolaEngine engine)
            {
                return engine.Parts.Where(p => p.Symbols.Any()).Select(p => p.Number).Sum();
            }

            public static int FindGearRatio(GondolaEngine engine)
            {
                int sum = 0;

                foreach (Vector2D coord in engine.Schematics.EnumerateCoords())
                {
                    if (engine.Schematics[coord] == '*')
                    {
                        var parts = engine.Parts.Where(p => p.Symbols.Where(s => s.Postion.X == coord.X && s.Postion.Y == coord.Y).Any()).ToArray();

                        if (parts.Count() == 2)
                        {
                            sum += parts[0].Number * parts[1].Number;
                        }
                    }
                }
                return sum;
            }
        }

        public class GondolaEngine
        {
            public SparseMap2D<char> Schematics { get; set; }
            public List<PartNumber> Parts { get; set; }

            public void ReadSchematics(IEnumerable<string> rawSchematics)
            {
                Schematics = new SparseMap2D<char>();
                Schematics.InitFromStringArray(rawSchematics.ToArray());
            }

            public List<Symbol> GetSurroundingSymbols(Vector2D coord)
            {
                List<Vector2D> surroundingCoords = Schematics.FilterValidCoords(coord.GetSurrounding());
                List<Symbol> symbols = new List<Symbol>();

                foreach (var surroundingCoord in surroundingCoords)
                {
                    if (!Schematics[surroundingCoord].IsNumber() && Schematics[surroundingCoord] != '.')
                    {
                        Symbol sym = new Symbol();
                        sym.SymbolChar = Schematics[surroundingCoord];
                        sym.Postion = surroundingCoord;
                        symbols.Add(sym);
                    }
                }
                return symbols;
            }

            public List<Symbol> GetSurroundingSymbols(int x, int y)
            {
                return GetSurroundingSymbols(new Vector2D(x, y));
            }

            public void Annotate()
            {
                Parts = new List<PartNumber>();

                int len = 1;
                foreach (Vector2D position in Schematics.EnumerateCoords())
                {
                    // Step over already processed positions
                    if (len > 1)
                    {
                        len--;
                        continue;
                    }

                    if (Schematics[position].IsNumber())
                    {
                        List<Symbol> symbols = new List<Symbol>();

                        string number = $"{Schematics[position]}";
                        symbols.AddRange(GetSurroundingSymbols(position));

                        while (position.X + len < Schematics.MaxX)
                        {
                            if (Schematics[position.X + len, position.Y].IsNumber())
                            {
                                number += $"{Schematics[position + Directions.Vector.Right * len]}";

                                var moreSymbols = GetSurroundingSymbols(position.X + len, position.Y);
                                foreach (var symbol in moreSymbols)
                                {
                                    if (!symbols.Where(s => s.Postion == symbol.Postion).Any())
                                    {
                                        symbols.Add(symbol);
                                    }
                                }
                                len++;
                            }
                            else
                                break;
                        }

                        PartNumber part = new PartNumber
                        {
                            Number = number.ToInt(),
                            Symbols = symbols,
                        };

                        Parts.Add(part);
                    }
                }
            }
        }

        public class PartNumber
        {
            public int Number { get; set; }
            public List<Symbol> Symbols { get; set; }
        }

        public class Symbol
        {
            public char SymbolChar { get; set; }
            public Vector2D Postion { get; set; }
        }
    }
}
