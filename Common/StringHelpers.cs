using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public static class StringHelpers
	{
		public static int ToInt(this string s)
		{
			int.TryParse(s, out int outVal);
			return outVal;
		}

		public static int ToInt(this char c)
		{
			int.TryParse(c.ToString(), out int outVal);
			return outVal;
		}

		public static long ToLong(this string s)
		{
			long.TryParse(s, out long outVal);
			return outVal;
		}

		public static ulong ToUlong(this string s)
		{
			ulong.TryParse(s, out ulong outVal);
			return outVal;
		}

		public static bool IsNumber(this string s)
		{
			return long.TryParse(s, out long result);
		}

		public static bool IsNumber(this char c)
		{
			return long.TryParse(c.ToString(), out long result);
		}

		public static List<string> JoinMultiline(this List<string> data, string joinchar)
		{
			StringBuilder builder = new StringBuilder();
			List<string> strings = new List<string>();
			foreach (string current in data)
			{
				if (!string.IsNullOrWhiteSpace(current))
					builder.Append(joinchar + current);
				else
				{
					strings.Add(builder.ToString().Trim());
					builder.Clear();
				}
			}
			if (builder.Length > 0)
				strings.Add(builder.ToString().Trim());

			return strings;
		}

		public static List<string> SplitOnNewline(this string me)
		{
			return me.Split(
			new[] { "\r\n", "\r", "\n" },
			StringSplitOptions.None
			).ToList();
		}
	}
}
