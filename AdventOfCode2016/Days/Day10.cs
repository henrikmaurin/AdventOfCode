using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Days
{
    public class Day10
    {
        private List<string> lines;
        private Bot[] Bots = new Bot[1000];
        private int[] output = new int[25];

        public Day10(bool demodata = false)
        {

            if (!demodata)
                lines = File.ReadAllText("data\\10.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                lines = File.ReadAllText("demodata\\10.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        public int Problem1()
        {
            int bot = 0;
            foreach (string line in lines.Where(l => l.StartsWith("bot")))
            {
                string[] instructions = line.Split(' ');
                int botNr = int.Parse(instructions[1]);
                int low = int.Parse(instructions[6]);
                int high = int.Parse(instructions[11]);

                Bot newBot = new Bot(ref Bots, ref output);
                newBot.Id = botNr;
                newBot.GiveLow = low;
                newBot.GiveHigh = high;

                Bots[botNr] = newBot;
            }

            foreach (string line in lines.Where(l => l.StartsWith("value")))
            {
                string[] instructions = line.Split(' ');
                int botNr = int.Parse(instructions[5]);
                int value = int.Parse(instructions[1]);


                int result = Bots[botNr].SetInput(value);
                if (result > 0)
                    return result;

            }

            return bot;
        }
        public int Problem2()
        {
            int bot = 0;
            foreach (string line in lines.Where(l => l.StartsWith("bot")))
            {
                string[] instructions = line.Split(' ');
                int botNr = int.Parse(instructions[1]);
                int low = int.Parse(instructions[6]);
                bool lowIsBin = instructions[5] == "output" ? true : false;
                int high = int.Parse(instructions[11]);
                bool highIsBin = instructions[10] == "output" ? true : false;

                Bot newBot = new Bot(ref Bots, ref output);
                newBot.Id = botNr;
                newBot.GiveLow = low;
                newBot.LowIsBin = lowIsBin;
                newBot.GiveHigh = high;
                newBot.HighIsBin = highIsBin;

                Bots[botNr] = newBot;
            }

            foreach (string line in lines.Where(l => l.StartsWith("value")))
            {
                string[] instructions = line.Split(' ');
                int botNr = int.Parse(instructions[5]);
                int value = int.Parse(instructions[1]);


                int result = Bots[botNr].SetInput(value, false);


            }

            return output[0] * output[1] * output[2];
        }

        private class Bot
        {
            private Bot[] _bots;
            private int[] _bins;

            public int[] inputs { get; set; }

            public int GiveLow { get; set; }
            public bool LowIsBin { get; set; } = false;
            public int GiveHigh { get; set; }
            public bool HighIsBin { get; set; } = false;
            public int usedInputs { get; set; } = 0;
            public int Id { get; set; }

            public Bot(ref Bot[] bots, ref int[] bins)
            {
                _bots = bots;
                _bins = bins;
                inputs = new int[2];
            }

            public int SetInput(int value, bool breakonmatch = true)
            {
                if (usedInputs == 2)
                    throw new Exception("Error inputs");

                inputs[usedInputs++] = value;

                if (usedInputs == 2)
                {
                    int low = inputs.OrderBy(i => i).First();
                    int high = inputs.OrderByDescending(i => i).First();

                    inputs[0] = 0;
                    inputs[1] = 0;
                    usedInputs = 0;

                    if (low == 17 && high == 61 && breakonmatch)
                        return Id;

                    int result = 0;

                    if (!LowIsBin)
                    {
                        result = _bots[GiveLow].SetInput(low, breakonmatch);
                        if (result > 0)
                            return result;
                    }
                    else
                    {
                        _bins[GiveLow] = low;
                    }

                    if (!HighIsBin)
                    {
                        result = _bots[GiveHigh].SetInput(high, breakonmatch);
                        if (result > 0)
                            return result;
                    }
                    else
                    {
                        _bins[GiveHigh] = high;
                    }

                }

                return -1;
            }
        }
    }
}
