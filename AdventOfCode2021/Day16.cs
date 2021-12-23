using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day16:DayBase
    {
        private BitsPacket topPacket;
        public int Problem1()
        {
            string instructions = input.GetDataCached(2021, 16);
               
            topPacket = new BitsPacket();
            topPacket.ParseHex(instructions);

            return SumVersions(topPacket);

        }
        public Int64 Problem2()
        {
            string instructions = input.GetDataCached(2021, 16);
            
            topPacket = new BitsPacket();
            topPacket.ParseHex(instructions);

            return topPacket.GetValue();

        }

        Int64 Sum(BitsPacket packet)
        {
            if (packet.TypeId == 4)
                return packet.Value.Value;

            return packet.SubPackets.Select(p => Sum(p)).Sum();
        }

        int SumVersions(BitsPacket packet)
        {
            if (packet.TypeId == 4)
                return packet.Version;

            return packet.Version+ packet.SubPackets.Select(p => SumVersions(p)).Sum();
        }

    }

    public class BitsPacket
    {
        public int Version { get; set; }
        public int TypeId { get; set; }
        public Int64? Value { get; set; }
        public int? LengthTypeId { get; set; }
        public List<BitsPacket> SubPackets { get; set; } = new List<BitsPacket>();
        public int SubPacketsLength { get; set; }
        public int SubPacketsCount { get; set; }
        public Int64 GetValue()
        {
            if (TypeId == 4)
                return Value.Value;

            List<Int64> valueList = SubPackets.Select(sp=>sp.GetValue()).ToList();


            switch (TypeId)
            {
                case 0:
                    return valueList.Sum();
                case 1:
                    return valueList.Aggregate((Int64)1, (acc, val) =>(Int64) acc * (Int64) val);
                case 2:
                    return valueList.Min();
                case 3:
                    return valueList.Max();
                case 5:
                    return valueList[0] > valueList[1] ? 1 : 0;
                case 6:
                    return valueList[0] < valueList[1] ? 1 : 0;
                case 7:
                    return valueList[0] == valueList[1] ? 1 : 0;
            }

            return 0;
        }

        public int ParseHex(string hexString)
        {
            string bits = HexToBinary(hexString);
            return Parse(bits);

        }

        public int Parse(string bits)
        {
            int parsePos = 0;
       
            string workBits = bits.Substring(parsePos, 3);
            parsePos += 3;
            Version = Convert.ToInt32(workBits, 2);


            workBits = bits.Substring(parsePos, 3);
            parsePos += 3;
            TypeId = Convert.ToInt32(workBits, 2);

            if (TypeId == 4)
            {
                workBits = bits.Substring(parsePos, 5);
                 bool keepReading = true;
                string bitValue = string.Empty;
                while (keepReading)
                {
                    parsePos += 5;
                    keepReading = workBits.StartsWith("1");
                    bitValue += workBits.Substring(1);
                    if (keepReading)
                        workBits = bits.Substring(parsePos, 5);
                   
                }
                Value = Convert.ToInt64(bitValue, 2);
            }
            else
            {
                workBits = bits.Substring(parsePos, 1);
                parsePos++;
                LengthTypeId = Convert.ToInt32(workBits, 2);
                int subPacketsBitsLengths = LengthTypeId == 1 ? 11 : 15;
                workBits = bits.Substring(parsePos, subPacketsBitsLengths);
                parsePos += subPacketsBitsLengths;
                

                BitsPacket subPacket = new BitsPacket();
                int parsedCount = subPacket.Parse(bits.Substring(parsePos));
                if (LengthTypeId == 0)
                {
                    SubPacketsLength = Convert.ToInt32(workBits, 2);
                    while (parsedCount < SubPacketsLength)
                    {
                        SubPackets.Add(subPacket);
                        subPacket = new BitsPacket();
                        parsedCount += subPacket.Parse(bits.Substring(parsePos + parsedCount));
                    }
                    SubPackets.Add(subPacket);
                    parsePos += parsedCount;
                }
                else
                {
                    SubPacketsCount = Convert.ToInt32(workBits, 2);
                    int count = 1;
                    while (count < SubPacketsCount)
                    {
                        count++;
                        SubPackets.Add(subPacket);
                        subPacket = new BitsPacket();
                        parsedCount += subPacket.Parse(bits.Substring(parsePos + parsedCount));
                    }
                    SubPackets.Add(subPacket);
                    parsePos += parsedCount;
                }

            }
            return parsePos;
        }



        public static string HexToBinary(string hexValue)
        {
            string binaryString = string.Empty;

            foreach (char c in hexValue)
            {
                switch (c)
                {
                    case '0': binaryString += "0000"; break;
                    case '1': binaryString += "0001"; break;
                    case '2': binaryString += "0010"; break;
                    case '3': binaryString += "0011"; break;
                    case '4': binaryString += "0100"; break;
                    case '5': binaryString += "0101"; break;
                    case '6': binaryString += "0110"; break;
                    case '7': binaryString += "0111"; break;
                    case '8': binaryString += "1000"; break;
                    case '9': binaryString += "1001"; break;
                    case 'A': binaryString += "1010"; break;
                    case 'B': binaryString += "1011"; break;
                    case 'C': binaryString += "1100"; break;
                    case 'D': binaryString += "1101"; break;
                    case 'E': binaryString += "1110"; break;
                    case 'F': binaryString += "1111"; break;
                }



            }
            return binaryString;
        }
    }

}
