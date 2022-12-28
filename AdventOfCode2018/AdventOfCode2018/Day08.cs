using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        public Queue<int> data { get; set; }
        public LicenceData TopData { get; private set; }

        private int totalsum;

        public Day08(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = new Queue<int>(testdata.IsSingleLine().Tokenize().Select(d => int.Parse(d)).ToList());
                return;
            }
            data = new Queue<int>(input.GetDataCached().IsSingleLine().Tokenize().Select(d => int.Parse(d)).ToList());
            totalsum = 0;
            TopData = Parse(data);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Total sum:{result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Summa: {result2}");
        }
        public int Problem1()
        {
            return totalsum;
        }

        public int Problem2()
        {
            return GetValue(TopData);
        }


        public LicenceData Parse(Queue<int> Data)
        {            
            LicenceData newData = new LicenceData();
            newData.Metadata = new List<int>();
            newData.Data = new List<LicenceData>();
            int nodes = Data.Dequeue();
            int metaDataEntries = Data.Dequeue();
            for (int i = 0; i < nodes; i++)
            {
                newData.Data.Add(Parse(Data));
            }
            for (int i = 0; i < metaDataEntries; i++)
            {
                int metadata = Data.Dequeue();
                totalsum += metadata;
                newData.Metadata.Add(metadata);
            }

            return newData;
        }

        public int GetValue(LicenceData data)
        {
            int sum = 0;
            if (data.Data.Count() == 0)
                return data.Metadata.Sum();


            foreach (int metadata in data.Metadata)
            {
                if (data.Data.Count() >= metadata)
                    sum += GetValue(data.Data[metadata - 1]);
            }

            return sum;
        }


    }

    public class LicenceData
    {
        public List<int> Metadata { get; set; }
        public List<LicenceData> Data { get; set; }
    }



}
