using AdventOfCode2020;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class UnitTestDay05
	{
		[TestMethod]
		public void Example1()
		{
			string seatData = "BFFFBBFRRR";

			Day05.Seat resultSeat = new Day05.Seat(seatData);
			Assert.AreEqual(567, resultSeat.Id);
		}
		[TestMethod]
		public void Example2()
		{
			string seatData = "FFFBBBFRRR";

			Day05.Seat resultSeat = new Day05.Seat(seatData);
			Assert.AreEqual(119, resultSeat.Id);
		}
		[TestMethod]
		public void Example3()
		{
			string seatData = "BBFFBBFRLL";

			Day05.Seat resultSeat = new Day05.Seat(seatData);
			Assert.AreEqual(820, resultSeat.Id);
		}

		[TestMethod]
		public void Example4()
		{
			string seatData = "FBFBBFFRLR";

			Day05.Seat resultSeat = new Day05.Seat(seatData);
			Assert.AreEqual(357, resultSeat.Id);
		}
	}
}
