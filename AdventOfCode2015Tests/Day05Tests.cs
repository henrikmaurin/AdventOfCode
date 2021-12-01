using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day05Tests
    {
        Day05 day;
        
        [TestInitialize]
        public void Init()
        {
            day = new Day05();
        }

        [TestMethod]
        public void TestMethod1()
        {
              string str = "ugknbfddgicrmopn";
            Assert.IsTrue(day.Nice(str));
        }

        [TestMethod]
        public void TestMethod2()
        {
             string str = "aaa";
            Assert.IsTrue(day.Nice(str));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string str = "jchzalrnumimnmhp";
            Assert.IsTrue(day.Naughty(str));
        }

        [TestMethod]
        public void TestMethod4()
        {
             string str = "haegwjzuvuyypxyu";
            Assert.IsTrue(day.Naughty(str));
        }

        [TestMethod]
        public void TestMethod5()
        {
              string str = "dvszwmarrgswjxmb";
            Assert.IsTrue(day.Naughty(str));
        }

        [TestMethod]
        public void TestMethod6()
        {
            string str = "qjhvhtzxzqqjkmpb";
            Assert.IsTrue(day.Nice2(str));
        }

        [TestMethod]
        public void TestMethod7()
        {
            string str = "xxyxx";
            Assert.IsTrue(day.Nice2(str));
        }

        [TestMethod]
        public void TestMethod8()
        {
            string str = "uurcxstgmygtbstg";
            Assert.IsTrue(day.Naughty2(str));
        }

        [TestMethod]
        public void TestMethod9()
        {
            string str = "ieodomkazucvgmuy";
            Assert.IsTrue(day.Naughty2(str));
        }

        [TestMethod]
        public void TestMethod10()
        {
            string str = "xyxy";
            Assert.IsTrue(day.NiceRuleDoubbleLettersTwice(str));
        }

        [TestMethod]
        public void TestMethod11()
        {
            string str = "aabcdefgaa";
            Assert.IsTrue(day.NiceRuleDoubbleLettersTwice(str));
        }

        [TestMethod]
        public void TestMethod12()
        {
            string str = "aaa";
            Assert.IsFalse(day.NiceRuleDoubbleLettersTwice(str));
        }

        [TestMethod]
        public void TestMethod13()
        {
            string str = "xyx";
            Assert.IsTrue(day.NiceLetterRepeatsOneLetterBetween(str));
        }

        [TestMethod]
        public void TestMethod14()
        {
            string str = "abcdefeghi";
            Assert.IsTrue(day.NiceLetterRepeatsOneLetterBetween(str));
        }

        [TestMethod]
        public void TestMethod15()
        {
            string str = "aaa";
            Assert.IsTrue(day.NiceLetterRepeatsOneLetterBetween(str));
        }
    }
}
