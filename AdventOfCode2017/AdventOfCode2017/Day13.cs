using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Days
{
    public class Day13 : AdventOfCode2017
    {
        public List<FirewallLayer> Firewall { get; set; }
        public Day13()
        {
            Firewall = new List<FirewallLayer>();
            string[] layers = SplitLines(ReadData("13.txt"));
            foreach (string layer in layers)
            {
                FirewallLayer newLayer = new FirewallLayer();
                newLayer.Layer = int.Parse(Tokenize(layer)[0].Replace(":", ""));
                newLayer.Range = int.Parse(Tokenize(layer)[1].Replace(":", ""));

                Firewall.Add(newLayer);
            }

        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            int severity = 0;
            foreach (FirewallLayer layer in Firewall)
            {
                if (layer.Layer > (layer.Range - 1) * 2 && layer.Layer % (layer.Range - 1) * 2 == 0)
                {
                    severity += layer.Layer * layer.Range;
                }
            }

            Console.WriteLine($"Severity: {severity}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

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

            Console.WriteLine($"Minimum delay: {delay - 1}");
        }

    }



    public class FirewallLayer
    {
        public int Layer { get; set; }
        public int Range { get; set; }
    }
}
