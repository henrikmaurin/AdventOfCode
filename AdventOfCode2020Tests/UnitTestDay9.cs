using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day9;

namespace Tests
{
	[TestClass]
	public class UnitTestDay9
	{
		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data 1")]
		public void Example1()
		{
			Sequence seq = new Sequence(25);
			for (int i = 1; i <= 25; i++)
				seq.Add(i);

			bool result = seq.Validate(26);
			Assert.IsTrue(result);
		}

		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data 2")]
		public void Example2()
		{
			Sequence seq = new Sequence(25);
			for (int i = 1; i <= 25; i++)
				seq.Add(i);

			bool result = seq.Validate(49);
			Assert.IsTrue(result);
		}

		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data 3")]
		public void Example3()
		{
			Sequence seq = new Sequence(25);
			for (int i = 1; i <= 25; i++)
				seq.Add(i);

			bool result = seq.Validate(100);
			Assert.IsFalse(result);
		}

		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data 4")]
		public void Example4()
		{
			Sequence seq = new Sequence(25);
			for (int i = 1; i <= 25; i++)
				seq.Add(i);

			bool result = seq.Validate(50);
			Assert.IsFalse(result);
		}

		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data sequence")]
		public void ExampleSequence()
		{
			List<long> testData = new List<long>
			{
				35,
				20,
				15,
				25,
				47,
				40,
				62,
				55,
				65,
				95,
				102,
				117,
				150,
				182,
				127,
				219,
				299,
				277,
				309,
				576
			};

			Sequence seq = new Sequence(5);

			long result = seq.FirstNonCompliant(testData);


			Assert.AreEqual(127, result);
		}

		[TestMethod("Day 9, Part 1")]
		[TestCategory("Answer")]
		public void Answer1()
		{
			int answer = 167829540;
			Assert.AreEqual(answer, Day9.Problem1());
		}

		[TestMethod("Day 9, Part 2")]
		[TestCategory("Example data sequence")]
		public void ExampleSequence2()
		{
			List<long> testData = new List<long>
			{
				35,
				20,
				15,
				25,
				47,
				40,
				62,
				55,
				65,
				95,
				102,
				117,
				150,
				182,
				127,
				219,
				299,
				277,
				309,
				576
			};

			Sequence seq = new Sequence(5);

			long result = seq.FindWeakness(127, testData);


			Assert.AreEqual(62, result);
		}

		[TestMethod("Day 9, Part 2")]
		[TestCategory("Answer")]
		public void Answer2()
		{
			int answer = 28045630;
			Assert.AreEqual(answer, Day9.Problem2());
		}

	}
}
