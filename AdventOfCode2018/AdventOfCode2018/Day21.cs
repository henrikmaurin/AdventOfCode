using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2018.ElfCode;

namespace AdventOfCode2018.Days
{
    public class Day21 : AdventOfCode2018
    {
        public List<int> reg { get; set; }
        public ElfCode Computer { get; set; }

        public Day21()
        {
            //Instructions = SplitLines(ReadData("21.txt")).ToList();
            reg = new List<int>();

            Computer = new ElfCode();
            Computer.CreateMachine(6);
            Computer.LoadAssembyCode("data\\21.txt");

        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            //Compile();

            BreakPoint bp = new BreakPoint
            {
                line = 28,
                Action = Breakpoint1
            };
            Computer.Run(bp);

            Console.WriteLine(Computer.register[3]);
        }

        public bool Breakpoint1()
        {
            return true;
        }

        public bool Breakpoint2()
        {
            if (!reg.Contains(Computer.register[3]))
            {
                reg.Add(Computer.register[3]);
                if (reg.Count() % 1000 == 0)
                {
                    Console.WriteLine(reg.Count());
                }
            }
            else
            {
                Console.WriteLine(reg.Last());
                Console.WriteLine(DateTime.Now);
                return true; ;
            }
            return false;

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
//            string op = Instructions[0];
//            Parse(op);
//            Instructions.RemoveAt(0);
            List<int> reg = new List<int>();
            int val = 0; ;

            bool dup = false;
            while (!dup)
            {
                val = Logic(val);
                if (!reg.Contains(val))
                {
                    reg.Add(val);
                }
                else
                {
                    dup = true;
                    Console.WriteLine(reg.Last());
                    return;
                }
            }
        }

        public void Problem2_BruteForce()
        {
            Console.WriteLine("Problem 2 brute force");
  //          Compile();

            Console.WriteLine(DateTime.Now);
            BreakPoint bp = new BreakPoint
            {
                line = 28,
                Action = Breakpoint2
            };
            Computer.Run(bp);

        }
 /*       public void Run_1()
        {
            if (CompiledCode == null)
            {
                return;
            }

            int codeLength = CompiledCode.Count() / 4;
            int ip = GetIp();
            while (ip >= 0 && ip < codeLength)
            {
                if (ip == 28)
                {
                    return;
                }

                Processor[CompiledCode[ip * 4]].Invoke(CompiledCode[ip * 4 + 1], CompiledCode[ip * 4 + 2], CompiledCode[ip * 4 + 3]);
                ip = IpInc();
            }
        }

        public void Run_2()
        {
            if (CompiledCode == null)
            {
                return;
            }

            List<int> reg = new List<int>();

            int codeLength = CompiledCode.Count() / 4;
            int ip = GetIp();
            while (ip >= 0 && ip < codeLength)
            {
                if (ip == 28)
                {
                    if (!reg.Contains(register[3]))
                    {
                        reg.Add(register[3]);
                        if (reg.Count() % 1000 == 0)
                        {
                            Console.WriteLine(reg.Count());
                        }
                    }
                    else
                    {
                        Console.WriteLine(reg.Last());
                        Console.WriteLine(DateTime.Now);
                        return;
                    }
                }
                Processor[CompiledCode[ip * 4]].Invoke(CompiledCode[ip * 4 + 1], CompiledCode[ip * 4 + 2], CompiledCode[ip * 4 + 3]);
                ip = IpInc();
            }
        }
        */
        public int Logic(int a)
        {
            a |= 0x10000;
            int b = 4921097;
            b += a & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            b += (a >> 8) & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            b += (a >> 16) & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            return b;


            /*    register[3] = register[3] + register[4];
                register[3] |= 16777215;
                register[3] *= 65899;
                register[3] |= 16777215;

                register[3] = register[1] >> 8;
                register[3] &= 256;

                register[3] = register[1] >> 16;
                register[3] &= 256;

                register[3] *= 65899;
                register[1] = register[3];*/
        }

    }
}
