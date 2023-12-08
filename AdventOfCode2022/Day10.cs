using Common;

namespace AdventOfCode2022
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        List<string> data;
        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            string result2 = MeasureExecutionTime(() => Problem2());
            Console.WriteLine($"P2: Result:{Environment.NewLine}{result2}");
        }
        public int Problem1()
        {
            return CountStrength(data, 20) + CountStrength(data, 60) + CountStrength(data, 100) + CountStrength(data, 140) + CountStrength(data, 180) + CountStrength(data, 220);
        }
        public string Problem2()
        {
            string matrix = Render(data);


            return matrix + Environment.NewLine + MatrixToText.Convert(matrix.SplitOnNewlineArray(), 5, '#', '.');
        }

        public int CountStrength(List<string> instructions, int cycles)
        {
            int cyclecounter = 0;
            int signalstrength = 1;
            int instructionCounter = 0;
            {
                while (cyclecounter + GetCycles(instructions[instructionCounter].Split(" ").First()) < cycles)
                {
                    switch (instructions[instructionCounter].Split(" ").First())
                    {
                        case "addx":
                            signalstrength += instructions[instructionCounter].Split(" ").Last().ToInt();
                            break;
                    }
                    cyclecounter += GetCycles(instructions[instructionCounter].Split(" ").First());
                    instructionCounter++;
                }
                return signalstrength * cycles;
            }


        }

        public string Render(List<string> instructions)
        {
            int cycle = 0;
            int registerX = 1;
            string result = string.Empty;

            int instructioncounter = 0;
            int executecounter = 0;
            int add = 0;

            while (instructioncounter < instructions.Count)
            {
                if (executecounter == 0)
                {
                    string instruction = instructions[instructioncounter].Split(" ").First();
                    executecounter = GetCycles(instruction);
                    if (instruction == "addx")
                        add = instructions[instructioncounter].Split(" ").Last().ToInt();
                    else
                        add = 0;
                    instructioncounter++;
                }

                if (cycle.IsBetween(registerX - 1, registerX + 1))
                    result += "#";
                else
                    result += ".";

                cycle++;
                if (cycle % 40 == 0)
                {
                    result += Environment.NewLine;
                    cycle = 0;
                }

                executecounter--;
                if (executecounter == 0)
                {
                    registerX += add;
                }
            }

            return result;
        }

        public int GetCycles(string instruction)
        {
            switch (instruction)
            {
                case "noop":
                    return 1;
                case "addx":
                    return 2;
            }
            return 0;
        }
    }
}
