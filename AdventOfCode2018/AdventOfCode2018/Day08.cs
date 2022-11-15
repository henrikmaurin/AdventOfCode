using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day08 : DayBase, IDay
    {
        public Day08() : base(2018, 8)
        {
            data = new Queue<int>(input.GetDataCached().IsSingleLine().Tokenize().Select(d => int.Parse(d)).ToList());
            Licenses = new List<LicenceData>();
            counter = 0;
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



        public Queue<int> data { get; set; }
        public List<LicenceData> Licenses { get; set; }
        public int counter { get; set; }

        private int totalsum;

        public LicenceData TopData { get; private set; }

        public LicenceData Parse(Queue<int> Data)
        {
            LicenceData newData = new LicenceData();
            newData.Id = counter++;
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
        public int Id { get; set; }
        public List<int> Metadata { get; set; }
        public List<LicenceData> Data { get; set; }


    }



}
