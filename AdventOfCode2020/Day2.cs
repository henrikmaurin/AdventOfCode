using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day2
	{
		public static int Problem1()
		{
			List<string> passwords = File.ReadAllLines("Data/Day2.txt").ToList();
			int result = ValidatePasswords(passwords);
			Console.WriteLine(result);
			return result;
		}

		public static int Problem2()
		{
			List<string> passwords = File.ReadAllLines("Data/Day2.txt").ToList();
			int result = ValidatePasswordsNew(passwords);
			Console.WriteLine(result);
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
