using System.Text;

using Microsoft.Extensions.Primitives;

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

		public static List<string> SplitOnDoubleNewline(this string me, bool removeEmptyLines = true)
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

		public static string[] SplitOnDoubleNewlineArray(this string me, bool removeEmptyLines = true)
		{
			if (me == null)
				return new string[0];

			var retval = me.Split(
			new[] { "\r\n\r\n", "\r\r", "\n\n" },
			StringSplitOptions.None
			);

			if (removeEmptyLines)
				retval = retval.Where(m => !string.IsNullOrEmpty(m)).ToArray();

			return retval;
		}

		public static string[] SplitOnWhitespace(this string me)
		{
			return System.Text.RegularExpressions.Regex.Split(me, @"\s+");
		}

		public static string ReplaceNewLine(this string me)
		{
			me = me.Replace("\r\n", Environment.NewLine);
			me = me.Replace("\r", Environment.NewLine);
			me = me.Replace("\n", Environment.NewLine);
			return me;
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

		public static char ToLower(this char indata)
		{
			return char.ToLower(indata);
		}

		public static char ToUpper(this char indata)
		{
			return char.ToUpper(indata);
		}

		public static bool IsUpper(this char indata)
		{
			return char.IsUpper(indata);
		}

		public static bool IsLower(this char indata)
		{
			return char.IsLower(indata);
		}

		public static string SafeSubstring(this string orig, int length)
		{
			return orig.Substring(0, orig.Length >= length ? length : orig.Length);
		}
		public static string SafeSubstringFromStart(this string orig, int start)
		{
			if(start >= orig.Length)
				return string.Empty;
			return orig.Substring( start , orig.Length-start);
		}

		public static string SafeSubstring(this string orig, int start, int length)
		{
			if (start >= orig.Length) return string.Empty;
			return orig.Substring(start, orig.Length >= start + length ? length : orig.Length - start);
		}

		public static int IndexOfAny(this string thisString, string[] strings, int startIndex=0)
		{
			List<int> result = strings.Select(s => thisString.IndexOf(s, startIndex)).Where(i=>i>=0).ToList();

			return result.Min();
		}

		public static int LastIndexOfAny(this string thisString, string[] strings, int startIndex = -1)
		{
			if (startIndex == -1)
				startIndex = thisString.Length-1;

			List<int> result = strings.Select(s => thisString.LastIndexOf(s, startIndex)).ToList();

			return result.Max();
		}

	}
}
