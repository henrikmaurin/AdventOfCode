using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day14 : DayBase, IDay
	{
		private List<string> instructions;
		public Day14() : base(2020, 14)
		{
			instructions = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			ulong result1 = Problem1();
			Console.WriteLine($"P1: Sum of values: {result1}");

			ulong result2 = Problem2();
			Console.WriteLine($"P2: Sum of values: {result2}");
		}
		public ulong Problem1()
		{
			PortComputer pc = new PortComputer();
			pc.ParseInstuctions(instructions);
			ulong result = pc.Sum();
			Console.WriteLine(result);
			return result;
		}

		public ulong Problem2()
		{
			PortComputer pc = new PortComputer();
			pc.ParseInstuctions2(instructions);
			ulong result = pc.Sum();
			Console.WriteLine(result);
			return result;
		}

		public class PortComputer
		{
			public char[] BitMask { get; set; }
			public Dictionary<ulong, Mem> Memory { get; set; }

			public PortComputer()
			{
				Memory = new Dictionary<ulong, Mem>();
			}

			public void SetMask(string mask)
			{
				BitMask = mask.ToCharArray();
			}

			public void ParseInstuctions(List<string> instructions)
			{
				foreach (string instruction in instructions)
				{
					List<string> command = instruction.Split("=").Select(i => i.Trim()).ToList();
					switch (command[0].Substring(0, 3))
					{
						case "mas":
							SetMask(command[1]);
							break;
						case "mem":
							ulong pos = command[0].Replace("mem[", "").Replace("]", "").ToUlong();
							if (!Memory.ContainsKey(pos))
							{
								Memory.Add(pos, new Mem(command[1]));
							}
							else
							{
								Memory[pos] = new Mem(command[1]);
							}
							Memory[pos].ApplyMask(BitMask);
							break;
					}
				}

			}

			public void ParseInstuctions2(List<string> instructions)
			{
				foreach (string instruction in instructions)
				{
					List<string> command = instruction.Split("=").Select(i => i.Trim()).ToList();
					switch (command[0].Substring(0, 3))
					{
						case "mas":
							SetMask(command[1]);
							break;
						case "mem":
							List<ulong> addresses = DecodeAdresses(command[0].Replace("mem[", "").Replace("]", ""), BitMask);
							foreach (ulong address in addresses)
							{
								if (!Memory.ContainsKey(address))
								{
									Memory.Add(address, new Mem(command[1]));
								}
								else
								{
									Memory[address] = new Mem(command[1]);
								}
							}
							break;
					}
				}

			}

			public List<ulong> DecodeAdresses(string mempos, char[] bitmask)
			{
				string binary = Convert.ToString(mempos.ToLong(), 2);
				char[] bits = binary.PadLeft(36, '0').ToCharArray();

				for (int i = 0; i < 36; i++)
				{
					switch (bitmask[i])
					{
						case 'X':
							bits[i] = 'X';
							break;
						case '1':
							bits[i] = '1';
							break;
					}
				}

				List<ulong> addresses = GetAllMatchng(bits, 0);

				return addresses;
			}

			List<ulong> GetAllMatchng(char[] mempos, int fromPos)
			{
				List<ulong> values = new List<ulong>();

				for (int i = fromPos; i < 36; i++)
				{
					if (mempos[i] == 'X')
					{
						char[] temp = (char[])mempos.Clone();
						temp[i] = '0';

						List<ulong> values1 = GetAllMatchng(temp, i + 1);
						temp[i] = '1';
						List<ulong> values2 = GetAllMatchng(temp, i + 1);

						values.AddRange(values1);
						values.AddRange(values2);
						return values;
					}
				}
				values.Add(Convert.ToUInt64(new string(mempos), 2));
				return values;
			}


			public ulong Sum()
			{
				var values = Memory.Select(m => m.Value.Value).ToList();
				return values.Aggregate((a, c) => a + c);
			}
		}

		public class Mem
		{
			public char[] Bits { get; set; }

			public Mem()
			{
				Bits = "000000000000000000000000000000000000".ToCharArray();
			}
			public Mem(string data)
			{
				string binary = Convert.ToString(data.ToLong(), 2);
				Bits = binary.PadLeft(36, '0').ToCharArray();
			}

			public Mem ApplyMask(char[] bitmask)
			{
				for (int i = 0; i < 36; i++)
				{
					switch (bitmask[i])
					{
						case '0':
							Bits[i] = '0';
							break;
						case '1':
							Bits[i] = '1';
							break;
					}
				}
				return this;
			}
			public ulong Value { get => Convert.ToUInt64(new string(Bits), 2); }
		}
	}
}
