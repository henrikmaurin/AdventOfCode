using System.Text;

namespace Common
{
	public static class StringHelpers
	{
		public static int FromBinary(this string s)
		{
			int result = Convert.ToInt32(s, 2);
			return result;
		}

		public static int[] FromBinary(this string[] s)
		{
			return s.Select(st => st.FromBinary()).ToArray();
		}


		public static int ToInt(this string s)
		{
			int.TryParse(s, out int outVal);
			return outVal;
		}

		public static int[] ToInt(this string[] s)
		{
			return s.Select(str => str.ToInt()).ToArray();
		}
		public static List<int> ToInt(this List<string> s)
		{
			return s.Select(str => str.ToInt()).ToList();
		}

		public static int ToInt(this char c)
		{
			int.TryParse(c.ToString(), out int outVal);
			return outVal;
		}

		public static int[] ToInt(this char[] c)
		{
			return c.Select(ch => ch.ToInt()).ToArray();
		}

		public static long ToLong(this string s)
		{
			long.TryParse(s, out long outVal);
			return outVal;
		}

		public static long[] ToLong(this string[] s)
		{
			return s.Select(str => str.ToLong()).ToArray();
		}

		public static List<long> ToLong(this List<string> s)
		{
			return s.Select(str => str.ToLong()).ToList();
		}

		public static ulong ToUlong(this string s)
		{
			ulong.TryParse(s, out ulong outVal);
			return outVal;
		}

		public static ulong[] ToUlong(this string[] s)
		{
			return s.Select(str => str.ToUlong()).ToArray();
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

		public static List<string> SplitOnNewline(this string me, bool removeEmptyLines = true)
		{
			return SplitOnNewlineArray(me, removeEmptyLines).ToList();
		}

		public static string[] SplitOnNewlineArray(this string me, bool removeEmptyLines = true)
		{
			if (me == null)
				return new string[0];

			var retval = me.Split(
			new[] { "\r\n", "\r", "\n" },
			StringSplitOptions.None
			);

			if (removeEmptyLines)
				retval = retval.Where(m => !string.IsNullOrEmpty(m)).ToArray();

			return retval;
		}

		public static string[][] GroupByEmptyLine(this string me)
		{
			List<string[]> list = new List<string[]>();
			List<string> group = new List<string>();
			foreach (string line in me.SplitOnNewlineArray(false))
			{
				if (group == null)
					group = new List<string>();

				if (string.IsNullOrEmpty(line))
				{
					list.Add(group.ToArray());
					group = null;
				}
				else
				{
					group.Add(line);
				}
			}

			if (group?.Count > 0)
				list.Add(group.ToArray());

			return list.ToArray();
		}

		public static string IsSingleLine(this string me)
		{
			return me.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
		}

		public static string[] Tokenize(this string indata)
		{
			return indata.Split(" ");
		}
		public static string[] Tokenize(this string indata, char tokenizer)
		{
			return indata.Split(tokenizer);
		}
	}
}
