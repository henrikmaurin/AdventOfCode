using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day16Tests
    {
        Day16 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day16();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string data = "D2FE28";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(6,bitsPacket.Version);
            Assert.AreEqual(4, bitsPacket.TypeId);
            Assert.AreEqual(2021, bitsPacket.Value);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string data = "38006F45291200";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(1, bitsPacket.Version);
            Assert.AreEqual(6, bitsPacket.TypeId);
            Assert.AreEqual(0, bitsPacket.LengthTypeId);
            Assert.AreEqual(27, bitsPacket.SubPacketsLength);
            Assert.AreEqual(2, bitsPacket.SubPackets.Count());
            Assert.AreEqual(10, bitsPacket.SubPackets[0].Value);
            Assert.AreEqual(20, bitsPacket.SubPackets[1].Value);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string data = "EE00D40C823060";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(7, bitsPacket.Version);
            Assert.AreEqual(3, bitsPacket.TypeId);
            Assert.AreEqual(1, bitsPacket.LengthTypeId);
            Assert.AreEqual(3, bitsPacket.SubPacketsCount);
            Assert.AreEqual(3, bitsPacket.SubPackets.Count());
            Assert.AreEqual(1, bitsPacket.SubPackets[0].Value);
            Assert.AreEqual(2, bitsPacket.SubPackets[1].Value);
            Assert.AreEqual(3, bitsPacket.SubPackets[2].Value);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string data = "C200B40A82";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(3, bitsPacket.GetValue());
                  }

        [TestMethod]
        public void TestMethod5()
        {
            string data = "04005AC33890";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(54, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod6()
        {
            string data = "880086C3E88112";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(7, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod7()
        {
            string data = "CE00C43D881120";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(9, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod8()
        {
            string data = "D8005AC2A8F0";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(1, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod9()
        {
            string data = "F600BC2D8F";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(0, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod10()
        {
            string data = "9C005AC2F8F0";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(0, bitsPacket.GetValue());
        }

        [TestMethod]
        public void TestMethod11()
        {
            string data = "9C0141080250320F1802104A08";

            BitsPacket bitsPacket = new BitsPacket();

            bitsPacket.ParseHex(data);

            Assert.AreEqual(1, bitsPacket.GetValue());
        }



    }
}
