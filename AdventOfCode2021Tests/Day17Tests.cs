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
    public class Day17Tests
    {

        Day17 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day17();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string data = "target area: x=20..30, y=-10..-5";

            HitZone hitZone = new HitZone();
            hitZone.Parse(data);

            Probe probe = new Probe();
            probe.Fire(7, 2);

            for (int i = 0; i < 7; i++)
                probe.Step();
            Assert.IsTrue(hitZone.IsHit(probe));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string data = "target area: x=20..30, y=-10..-5";

            HitZone hitZone = new HitZone();
            hitZone.Parse(data);

            Probe probe = new Probe();
            probe.Fire(6, 3);

            for (int i = 0; i < 8; i++)
                probe.Step();
            Assert.IsFalse(hitZone.IsHit(probe));
            probe.Step();
            Assert.IsTrue(hitZone.IsHit(probe));
            Assert.IsFalse(hitZone.HasMissed(probe));
            probe.Step();
            Assert.IsTrue(hitZone.HasMissed(probe));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string data = "target area: x=20..30, y=-10..-5";

            day.Parse(data);
            Assert.AreEqual(45, day.FindHighest());
        }

        [TestMethod]
        public void TestMethod4()
        {
            string data = "target area: x=20..30, y=-10..-5";

            day.Parse(data);
            Assert.AreEqual(112, day.FindAll());
        }
    }
}
