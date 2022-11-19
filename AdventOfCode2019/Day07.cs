using Common;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day07 : DayBase, IDay
    {
        private long[] opCodes;

        public Day07() : base(2019, 7)
        {
            opCodes = input.GetDataCached().Tokenize(',').ToLong();
        }

        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Highest signal: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Highest signal: {result2}");
        }

        public long Problem1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            List<long> output = new List<long>();
            long max = 0;
            for (int ph1 = 0; ph1 < 5; ph1++)
                for (int ph2 = 0; ph2 < 5; ph2++)
                    for (int ph3 = 0; ph3 < 5; ph3++)
                        for (int ph4 = 0; ph4 < 5; ph4++)
                            for (int ph5 = 0; ph5 < 5; ph5++)
                            {
                                if (ph2 != ph1 && ph3 != ph1 && ph3 != ph2 && ph4 != ph1 && ph4 != ph2 && ph4 != ph3 && ph5 != ph1 && ph5 != ph2 && ph5 != ph3 && ph5 != ph4)
                                {
                                    long outputVal;
                                    computer.InitRam(opCodes);
                                    output.Clear();
                                    computer.Run(new List<long> { ph1, 0 }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<long> { ph2, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<long> { ph3, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<long> { ph4, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer.InitRam(opCodes);
                                    computer.Run(new List<long> { ph5, outputVal }, output);
                                    if (output[0] > max)
                                        max = output[0];

                                }




                            }

            return max;
        }

        public long Problem2()
        {
            IntcodeComputer computer1 = new IntcodeComputer();
            IntcodeComputer computer2 = new IntcodeComputer();
            IntcodeComputer computer3 = new IntcodeComputer();
            IntcodeComputer computer4 = new IntcodeComputer();
            IntcodeComputer computer5 = new IntcodeComputer();
            List<long> output = new List<long>();
            long max = 0;
            for (int ph1 = 5; ph1 < 10; ph1++)
                for (int ph2 = 5; ph2 < 10; ph2++)
                    for (int ph3 = 5; ph3 < 10; ph3++)
                        for (int ph4 = 5; ph4 < 10; ph4++)
                            for (int ph5 = 5; ph5 < 10; ph5++)
                            {
                                if (ph2 != ph1 && ph3 != ph1 && ph3 != ph2 && ph4 != ph1 && ph4 != ph2 && ph4 != ph3 && ph5 != ph1 && ph5 != ph2 && ph5 != ph3 && ph5 != ph4)
                                {
                                    long outputVal;
                                    computer1.InitRam(opCodes);
                                    computer2.InitRam(opCodes);
                                    computer3.InitRam(opCodes);
                                    computer4.InitRam(opCodes);
                                    computer5.InitRam(opCodes);

                                    output.Clear();
                                    computer1.Run(new List<long> { ph1, 0 }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer2.Run(new List<long> { ph2, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer3.Run(new List<long> { ph3, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer4.Run(new List<long> { ph4, outputVal }, output);
                                    outputVal = output[0];
                                    output.Clear();
                                    computer5.Run(new List<long> { ph5, outputVal }, output);
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
