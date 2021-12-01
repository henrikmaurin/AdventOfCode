using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public class Day7 : Days
    {
        private Int64[] opCodes;

        public Day7() : base()
        {
            string filename = Path.Combine(path, "Day7\\Program.txt");
            opCodes = File.ReadAllText(filename).Split(",").Select(l => Int64.Parse(l)).ToArray();
        }

        public Int64 Problem1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            List<Int64> output = new List<Int64>();
            Int64 max = 0;
            for (int ph1 = 0; ph1 < 5; ph1++)
                for (int ph2 = 0; ph2 < 5; ph2++)
                    for (int ph3 = 0; ph3 < 5; ph3++)
                        for (int ph4 = 0; ph4 < 5; ph4++)
                            for (int ph5 = 0; ph5 < 5; ph5++)
                            {
                                if (ph2 != ph1 && ph3 != ph1 && ph3 != ph2 && ph4 != ph1 && ph4 != ph2 && ph4 != ph3 && ph5 != ph1 && ph5 != ph2 && ph5 != ph3 && ph5 != ph4)
                                {
                                    Int64 outputVal;
                                    computer.InitRam(opCodes);
                                    output.Clear();
                                    computer.Run(new List<Int64> { ph1, 0 }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<Int64> { ph2, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<Int64> { ph3, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<Int64> { ph4, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<Int64> { ph5, outputVal }, output);
                                    if (output[0] > max)
                                        max = output[0];

                                }




                            }

            return max;
        }

        public Int64 Problem2()
        {
            IntcodeComputer computer1 = new IntcodeComputer();
            IntcodeComputer computer2 = new IntcodeComputer();
            IntcodeComputer computer3 = new IntcodeComputer();
            IntcodeComputer computer4 = new IntcodeComputer();
            IntcodeComputer computer5 = new IntcodeComputer();
            List<Int64> output = new List<Int64>();
            Int64 max = 0;
            for (int ph1 = 5; ph1 < 10; ph1++)
                for (int ph2 = 5; ph2 < 10; ph2++)
                    for (int ph3 = 5; ph3 < 10; ph3++)
                        for (int ph4 = 5; ph4 < 10; ph4++)
                            for (int ph5 = 5; ph5 < 10; ph5++)
                            {
                                if (ph2 != ph1 && ph3 != ph1 && ph3 != ph2 && ph4 != ph1 && ph4 != ph2 && ph4 != ph3 && ph5 != ph1 && ph5 != ph2 && ph5 != ph3 && ph5 != ph4)
                                {
                                    Int64 outputVal;
                                    computer1.InitRam(opCodes);
                                    computer2.InitRam(opCodes);
                                    computer3.InitRam(opCodes);
                                    computer4.InitRam(opCodes);
                                    computer5.InitRam(opCodes);

                                    output.Clear();
                                    computer1.Run(new List<Int64> { ph1, 0 }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer2.Run(new List<Int64> { ph2, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer3.Run(new List<Int64> { ph3, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer4.Run(new List<Int64> { ph4, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer5.Run(new List<Int64> { ph5, outputVal }, output);
                                    outputVal = output[0];

                                    while (computer1.running)
                                    {
                                        output.Clear();
                                        computer1.Resume(outputVal);
                                        outputVal = output[0];
                                        output.Clear();
                                        computer2.Resume(outputVal);
                                        outputVal = output[0];
                                        output.Clear();
                                        computer3.Resume(outputVal);
                                        outputVal = output[0];
                                        output.Clear();
                                        computer4.Resume(outputVal);
                                        outputVal = output[0];
                                        output.Clear();
                                        computer5.Resume(outputVal);
                                        outputVal = output[0];
                                    }



                                    if (output[0] > max)
                                        max = outputVal;

                                }




                            }

            return max;
        }

    }
}
