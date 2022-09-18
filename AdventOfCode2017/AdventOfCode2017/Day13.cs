using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    public class Day13 : DayBase, IDay
    {
        public List<FirewallLayer> Firewall { get; set; }
        public Day13() : base(2017, 13)
        {
            Firewall = new List<FirewallLayer>();
            string[] layers = input.GetDataCached().SplitOnNewlineArray();
            foreach (string layer in layers)
            {
                FirewallLayer newLayer = new FirewallLayer();
                newLayer.Layer = int.Parse(layer.Tokenize()[0].Replace(":", ""));
                newLayer.Range = int.Parse(layer.Tokenize()[1].Replace(":", ""));

                Firewall.Add(newLayer);
            }

        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Severity: {result1}");

            ulong result2 = Problem2();
            Console.WriteLine($"P2: Minimum delay: {result2}");
        }

        public int Problem1()
        {
            int severity = 0;
            foreach (FirewallLayer layer in Firewall)
            {
                if (layer.Layer > (layer.Range - 1) * 2 && layer.Layer % (layer.Range - 1) * 2 == 0)
                {
                    severity += layer.Layer * layer.Range;
                }
            }

            return severity;
        }

        public ulong Problem2()
        {
            ulong delay = 0;

            int severity = int.MaxValue;
            //  int[] layers = Firewall.Select(fw => fw.Layer).ToArray();

            while (severity > 0)
            {

                severity = 0;
                for (int layercounter = 0; layercounter < Firewall.Count; layercounter++)
                {
                    if (((ulong)Firewall[layercounter].Layer + delay) % ((ulong)(Firewall[layercounter].Range - 1) * 2) == 0)
                    {
                        severity = 1;
                        layercounter = Firewall.Count;
                    }
                }
                delay++;
            }

            return delay - 1;
        }

    }



    public class FirewallLayer
    {
        public int Layer { get; set; }
        public int Range { get; set; }
    }
}
