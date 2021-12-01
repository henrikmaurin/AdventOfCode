using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class UnitTestDay5
	{
		[TestMethod]
		public void Example1()
		{
			string seatData = "BFFFBBFRRR";

			Day5.Seat resultSeat = new Day5.Seat(seatData);
			Assert.AreEqual(567, resultSeat.Id);
		}
		[TestMethod]
		public void Example2()
		{
			string seatData = "FFFBBBFRRR";

			Day5.Seat resultSeat = new Day5.Seat(seatData);
			Assert.AreEqual(119, resultSeat.Id);
		}
		[TestMethod]
		public void Example3()
		{
			string seatData = "BBFFBBFRLL";

			Day5.Seat resultSeat = new Day5.Seat(seatData);
			Assert.AreEqual(820, resultSeat.Id);
		}

		[TestMethod]
		public void Example4()
		{
			string seatData = "FBFBBFFRLR";

			Day5.Seat resultSeat = new Day5.Seat(seatData);
			Assert.AreEqual(357, resultSeat.Id);
		}

		[TestMethod]
		public void Problem1()
		{
			Assert.AreEqual(801, Day5.Problem1());
		}

		[TestMethod]
		public void Problem2()
		{
			Assert.AreEqual(597, Day5.Problem2());
		}
	}
}
