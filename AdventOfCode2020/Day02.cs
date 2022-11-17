using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day02 : DayBase, IDay
	{
		private List<string> passwords;
		public Day02() : base(2020, 2)
		{
			passwords = input.GetDataCached().SplitOnNewline();
		}
		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Valid passwords: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Valid passwords: {result2}");
		}
		public int Problem1()
		{
			int result = ValidatePasswords(passwords);
			return result;
		}

		public int Problem2()
		{
			int result = ValidatePasswordsNew(passwords);
			return result;
		}

		public static bool ValidatePassword(string passwordstring)
		{
			PasswordPolicy policy = DecodeString(passwordstring);

			int count = policy.Password.ToCharArray().Where(p => p == policy.Char).Count();

			if (count < policy.Lower || count > policy.Upper)
				return false;
			return true;
		}

		public static int ValidatePasswords(List<string> passwordStrings)
		{
			return passwordStrings.Where(p => ValidatePassword(p)).Count();
		}

		public static int ValidatePasswordsNew(List<string> passwordStrings)
		{
			return passwordStrings.Where(p => ValidatePasswordNew(p)).Count();
		}

		public static PasswordPolicy DecodeString(string passwordstring)
		{
			PasswordPolicy policy = new PasswordPolicy();

			List<string> splitString = passwordstring.Split(" ").ToList();
			policy.Lower = splitString[0].Split("-").First().ToInt();
			policy.Upper = splitString[0].Split("-").Last().ToInt();
			policy.Char = splitString[1].First();
			policy.Password = splitString[2];

			return policy;
		}

		public static bool ValidatePasswordNew(string passwordstring)
		{
			PasswordPolicy policy = DecodeString(passwordstring);
			int count = 0;
			if (policy.Password[policy.Lower - 1] == policy.Char)
				count++;

			if (policy.Password[policy.Upper - 1] == policy.Char)
				count++;

			return count == 1;
		}

		public class PasswordPolicy
		{
			public int Lower { get; set; }
			public int Upper { get; set; }
			public char Char { get; set; }
			public string Password { get; set; }
		}
	}
}
