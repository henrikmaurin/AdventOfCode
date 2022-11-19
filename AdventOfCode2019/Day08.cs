using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day08 : DayBase, IDay
    {
        List<string> layerData;
        public Day08() : base(2019, 8)
        {
            int x = 25;
            int y = 6;
            int takesize = x * y;

            string text = input.GetDataCached();
            layerData = new List<string>();
            for (int i = 0; i < text.Length - 1; i += takesize)
            {
                layerData.Add(text.Substring(i, takesize));
            }

        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Checksum: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Checksum: {result2}");
        }

        public int Problem1()
        {
            int max0 = int.MaxValue;
            int maxLayer = 0;
            string maxLayerData = "";
            int i = 0;
            foreach (string data in layerData)
            {
                int ch = data.ToCharArray().Where(c => c == '0').Count();
                if (ch < max0)
                {
                    max0 = ch;
                    maxLayer = i;
                    maxLayerData = data;
                }
                i++;
            }
            return maxLayerData.ToCharArray().Where(c => c == '1').Count() * maxLayerData.ToCharArray().Where(c => c == '2').Count();
        }
        public int Problem2()
        {
            int x = 25;
            int y = 6;
            int takesize = x * y;
            char[] message = new string('2', takesize).ToCharArray();

            foreach (string layer in layerData)
            {
                for (int i = 0; i < layer.Length; i++)
                {
                    if (message[i] == '2')
                        message[i] = layer[i];
                }
            }
            int j = 0;
            Console.WriteLine();
            for (int yp = 0; yp < y; yp++)
            {
                for (int xp = 0; xp < x; xp++)
                {
                    Console.Write(message[j++] == '1' ? '*' : ' ');
                }
                Console.WriteLine();
            }

            return 0;
        }
    }

    internal class Layer
    {
        int[] data;
    }
}
