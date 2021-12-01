using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day8 : AdventOfCode2018
    {
        public Day8()
        {
            data = new Queue<int>(Tokenize(ReadData("8.txt")).Select(d => int.Parse(d)).ToList());
            Licenses = new List<LicenceData>();
            counter = 0;
            totalsum = 0;
            TopData = Parse(data);
        }


        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            

            Console.WriteLine($"Resultat: {totalsum}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            int sum = GetValue(TopData);

            Console.WriteLine($"Summa: {sum}");
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
                newData.Data.Add(Parse( Data));
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


            foreach(int metadata in data.Metadata)
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
