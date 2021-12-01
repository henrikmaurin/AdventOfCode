using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class UnitTestDay18
	{

		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			string testData = "1 + 2 * 3 + 4 * 5 + 6";
			long result = CustomMath.Parse(testData);

			Assert.AreEqual(71, result);
		}

		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example 2")]
		public void Example2()
		{
			string testData = "2 * 3 + (4 * 5)";
			long result = CustomMath.Parse(testData);

			Assert.AreEqual(26, result);
		}

		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example 3")]
		public void Example3()
		{
			string testData = "5 + (8 * 3 + 9 + 3 * 4 * 3)";
			long result = CustomMath.Parse(testData);

			Assert.AreEqual(437, result);
		}

		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example 4")]
		public void Example4()
		{
			string testData = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
			long result = CustomMath.Parse(testData);

			Assert.AreEqual(12240, result);
		}

		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example 5")]
		public void Example5()
		{
			string testData = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
			long result = CustomMath.Parse(testData);

			Assert.AreEqual(13632, result);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example 1")]
		public void Example21()
		{
			string testData = "1+2*3+4*5+6";

			CustomMath math = new CustomMath();

			long result = math.ParseUsingStack(testData);

			Assert.AreEqual(231, result);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example 2")]
		public void Example22()
		{
			string testData = "2 * 3 + (4 * 5)";
			CustomMath math = new CustomMath();
			long result = math.ParseUsingStack(testData);

			Assert.AreEqual(46, result);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example 3")]
		public void Example23()
		{
			string testData = "5 + (8 * 3 + 9 + 3 * 4 * 3)";
			CustomMath math = new CustomMath();
			long result = math.ParseUsingStack(testData);

			Assert.AreEqual(1445, result);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example 4")]
		public void Example24()
		{
			string testData = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
			CustomMath math = new CustomMath();
			long result = math.ParseUsingStack(testData);

			Assert.AreEqual(669060, result);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example 5")]
		public void Example25()
		{
			string testData = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
			CustomMath math = new CustomMath();
			long result = math.ParseUsingStack(testData);

			Assert.AreEqual(23340, result);
		}
	}
}
