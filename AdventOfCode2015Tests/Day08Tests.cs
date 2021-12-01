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
    public class Day08Tests
    {

        private Day08 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day08();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string data = "\"\"";
            LineInfo result = day.Count(data);
            Assert.AreEqual(2, result.CodeChars);
            Assert.AreEqual(0, result.MemChars);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string data = "\"abc\"";
            LineInfo result = day.Count(data);
            Assert.AreEqual(5, result.CodeChars);
            Assert.AreEqual(3, result.MemChars);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string data = "\"aaa\\\"aaa\"";
            LineInfo result = day.Count(data);
            Assert.AreEqual(10, result.CodeChars);
            Assert.AreEqual(7, result.MemChars);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string data = "\"\\x27\"";
            LineInfo result = day.Count(data);
            Assert.AreEqual(6, result.CodeChars);
            Assert.AreEqual(1, result.MemChars);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string data = "\"\"";
            LineInfo result = day.Expand(data);
            Assert.AreEqual(2, result.CodeChars);
            Assert.AreEqual(6, result.MemChars);
        }

        [TestMethod]
        public void TestMethod6()
        {
            string data = "\"abc\"";
            LineInfo result = day.Expand(data);
            Assert.AreEqual(5, result.CodeChars);
            Assert.AreEqual(9, result.MemChars);
        }

        [TestMethod]
        public void TestMethod7()
        {
            string data = "\"aaa\\\"aaa\"";
            LineInfo result = day.Expand(data);
            Assert.AreEqual(10, result.CodeChars);
            Assert.AreEqual(16, result.MemChars);
        }

        [TestMethod]
        public void TestMethod8()
        {
            string data = "\"\\x27\"";
            LineInfo result = day.Expand(data);
            Assert.AreEqual(6, result.CodeChars);
            Assert.AreEqual(11, result.MemChars);
        }

    }


}