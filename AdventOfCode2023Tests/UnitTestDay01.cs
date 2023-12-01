using AdventOfCode2023;
using Common;

using static AdventOfCode2023.Day01;

namespace Tests
{
	[TestClass]
	public class UnitTestDay01
	{
		//private Day01 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			//data = @"";
			//testdata = data.SplitOnNewlineArray(false);

			//day = new Day01(data);
		}


		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		[DataRow(12, "1abc2")]
		[DataRow(38, "pqr3stu8vwx")]
		[DataRow(15, "a1b2c3d4e5f")]
		[DataRow(77, "treb7uchet")]
		public void Part1_Examples(int result, string data)
		{
			int sum = DocumentDecoder.Findfirst(data) * 10 + DocumentDecoder.FindLast(data);

			Assert.AreEqual(result, sum);
		}

		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]		
		public void Part1_Examples_Sum()
		{
			List<string> documentLines = new List<string> { "1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet" };
			DecoderElf elf = new DecoderElf();
			
			int sum = elf.DecodeAllLines(documentLines);

			Assert.AreEqual(142, sum);
		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Example data")]
		[DataRow(29, "two1nine")]
		[DataRow(83, "eightwothree")]
		[DataRow(13, "abcone2threexyz")]
		[DataRow(24, "xtwone3four")]
		[DataRow(42, "4nineeightseven2")]
		[DataRow(14, "zoneight234")]
		[DataRow(76, "7pqrstsixteen")]
		public void Part2_Examples(int result, string data)
		{
			data = DocumentDecoder.ReplaceWordNumbers(data);
			int sum = DocumentDecoder.Findfirst(data) * 10 + DocumentDecoder.FindLast(data);

			Assert.AreEqual(result, sum);
		}

		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		public void Part2_Examples_Sum()
		{
			List<string> documentLines = new List<string> { "two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234" , "7pqrstsixteen" };
			DecoderElf elf = new DecoderElf();

			int sum = elf.DecodeAllLines(documentLines,true);

			Assert.AreEqual(281, sum);
		}
	}
}
