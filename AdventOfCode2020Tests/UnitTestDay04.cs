﻿using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day04;

namespace Tests
{
	[TestClass]
	public class UnitTestDay04
	{

		[TestMethod("Day 4, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			List<string> passportData = new List<string>
			{
				"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
				"byr:1937 iyr:2017 cid:147 hgt:183cm",
				"",
				"iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
				"hcl:#cfa07d byr:1929",
				" ",
				"hcl:#ae17e1 iyr:2013",
				"eyr:2024",
				"ecl:brn pid:760753108 byr:1931",
				"hgt:179cm",
				"",
				"hcl:#cfa07d eyr:2025 pid:166559648",
				"iyr:2011 ecl:brn hgt:59in"
			};

			List<Passport> passports = Passport.ListFactory(passportData);

			int result = ValidPassports(passports);

			Assert.AreEqual(2, result);
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Example data 1")]
		public void Part2_InValid()
		{
			List<string> passportData = new List<string>
			{
				"eyr:1972 cid:100",
				"hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
				"",
				"iyr:2019",
				"hcl:#602927 eyr:1967 hgt:170cm",
				"ecl:grn pid:012533040 byr:1946",
				"",
				"hcl:dab227 iyr:2012",
				"ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
				"   ",
				"hgt:59cm ecl:zzz",
				"eyr:2038 hcl:74454a iyr:2023",
				"pid:3556412378 byr:2007"
			};

			List<Passport> passports = Passport.ListFactory(passportData);

			int result = ValidPassports(passports, true);

			Assert.AreEqual(0, result);
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Example data 2")]
		public void Part2_Valid()
		{
			List<string> passportData = new List<string>
			{
				"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
				"hcl:#623a2f",
				"   ",
				"eyr:2029 ecl:blu cid:129 byr:1989",
				"iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
				"",
				"hcl:#888785",
				"hgt:164cm byr:2001 iyr:2015 cid:88",
				"pid:545766238 ecl:hzl",
				"eyr:2022",
				"",
				"iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
			};

			List<Day04.Passport> passports = Passport.ListFactory(passportData);

			int result = Day04.ValidPassports(passports, true);

			Assert.AreEqual(4, result);
		}
	}
}
