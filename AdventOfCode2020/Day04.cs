using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
	public class Day04 : DayBase, IDay
	{
		private List<string> passportData;
		public Day04() : base(2020, 4)
		{
			passportData = input.GetDataCached().SplitOnNewline(false);
		}
		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Number of passports: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Number of passports: {result2}");
		}

		public int Problem1()
		{
			List<Passport> passports = Passport.ListFactory(passportData);

			int result = ValidPassports(passports);
			return result;
		}

		public int Problem2()
		{
			List<Passport> passports = Passport.ListFactory(passportData);

			int result = ValidPassports(passports, true);
			return result;
		}

		public static int ValidPassports(List<Passport> passports, bool higherSecurity = false)
		{
			return passports.Where(p => p.Validate(higherSecurity)).Count();
		}

		public class Passport
		{
			private static List<string> validColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
			public int? BirthYear { get; set; }
			public int? IssueYear { get; set; }
			public int? ExpirationYear { get; set; }
			public string Height { get; set; }
			public string HairColor { get; set; }
			public string EyeColor { get; set; }
			public string PassportId { get; set; }
			public int? CountryId { get; set; }

			public Passport(string passportData)
			{
				List<string> props = passportData.Split(" ").ToList();

				foreach (string prop in props)
				{
					string p = prop.Split(":").First();
					string d = prop.Split(":").ElementAt(1);
					switch (p)
					{
						case "byr":
							BirthYear = d.ToInt();
							break;
						case "iyr":
							IssueYear = d.ToInt();
							break;
						case "eyr":
							ExpirationYear = d.ToInt();
							break;
						case "hgt":
							Height = d;
							break;
						case "hcl":
							HairColor = d;
							break;
						case "ecl":
							EyeColor = d;
							break;
						case "pid":
							PassportId = d;
							break;
						case "cid":
							CountryId = d.ToInt();
							break;
					}
				}
			}

			public static Passport Factory(string passportData)
			{
				List<string> props = passportData.Split(" ").ToList();
				Passport retPassport = new Passport(passportData);

				return retPassport;
			}
			public static List<Passport> ListFactory(List<string> passportData)
			{
				List<string> joined = passportData.JoinMultiline(" ");
				return joined.Select(p => new Passport(p)).ToList();
			}

			public bool Validate(bool strictSecurity = false)
			{
				if (!IsValidBirthYear(strictSecurity))
					return false;

				if (!IsValidIssueYear(strictSecurity))
					return false;

				if (!IsValidExpirationYear(strictSecurity))
					return false;

				if (!IsValidHeight(strictSecurity))
					return false;

				if (!IsValidHairColor(strictSecurity))
					return false;

				if (!IsValidEyeColor(strictSecurity))
					return false;

				if (!IsValidPassportId(strictSecurity))
					return false;

				if (!IsValidCountryId(strictSecurity))
					return false;

				return true;
			}

			public bool IsValidBirthYear(bool strictSecurity)
			{
				if (BirthYear == null)
					return false;
				if (strictSecurity)
				{
					if (BirthYear < 1920)
						return false;
					if (BirthYear > 2002)
						return false;
				}
				return true;
			}
			public bool IsValidIssueYear(bool strictSecurity)
			{
				if (IssueYear == null)
					return false;
				if (strictSecurity)
				{
					if (IssueYear < 2010)
						return false;
					if (IssueYear > 2020)
						return false;
				}
				return true;
			}
			public bool IsValidExpirationYear(bool strictSecurity)
			{
				if (ExpirationYear == null)
					return false;
				if (strictSecurity)
				{
					if (ExpirationYear < 2020)
						return false;
					if (ExpirationYear > 2030)
						return false;
				}
				return true;
			}
			public bool IsValidHeight(bool strictSecurity)
			{
				if (Height == null)
					return false;
				if (strictSecurity)
				{
					if (Height.EndsWith("cm"))
					{
						if (Height.Replace("cm", "").ToInt() < 150)
							return false;
						if (Height.Replace("cm", "").ToInt() > 193)
							return false;
					}
					else if (Height.EndsWith("in"))
					{
						if (Height.Replace("in", "").ToInt() < 59)
							return false;
						if (Height.Replace("in", "").ToInt() > 76)
							return false;
					}
					else
						return false;
				}
				return true;
			}
			public bool IsValidHairColor(bool strictSecurity)
			{
				if (HairColor == null)
					return false;
				if (strictSecurity && !Regex.IsMatch(HairColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"))
					return false;
				return true;
			}
			public bool IsValidEyeColor(bool strictSecurity)
			{
				if (EyeColor == null)
					return false;
				if (strictSecurity && !validColors.Contains(EyeColor))
					return false;
				return true;
			}
			public bool IsValidPassportId(bool strictSecurity)
			{
				if (PassportId == null)
					return false;
				if (strictSecurity && !Regex.IsMatch(PassportId, "^(?:[0-9]{9})$"))
					return false;
				return true;
			}
			public bool IsValidCountryId(bool strictSecurity)
			{
				return true;
			}
		}
	}
}
