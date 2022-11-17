using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day08 : DayBase, IDay
	{
		private List<string> programCode;

		public Day08() : base(2020, 8)
		{
			programCode = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Accumulator: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Accumulator: {result2}");
		}
		public int Problem1()
		{
			Computer computer = new Computer();
			computer.Load(programCode);

			int result = computer.RunUntilRepeat();

			return result;
		}

		public int Problem2()
		{
			Computer computer = new Computer();
			computer.Load(programCode);

			int result = ModifyUntilExecuteToEnd(computer);

			return result;
		}

		public static int ModifyUntilExecuteToEnd(Computer computer)
		{
			int pos = 0;
			string currentInstruction;
			while (pos < computer._program.Count)
			{
				currentInstruction = computer._program[pos].Instuction;
				if (currentInstruction == "nop")
					computer._program[pos].Instuction = "jmp";
				else if (currentInstruction == "jmp")
					computer._program[pos].Instuction = "nop";
				else
				{
					pos++;
					continue;
				}

				int result = computer.RunUntilEnd();
				if (result != int.MinValue)
					return result;

				computer._program[pos].Instuction = currentInstruction;
				computer.Reset();
				pos++;

			}
			return int.MinValue;
		}


		public class Computer
		{
			private int _accumulator;
			private int _pc;
			public List<Instruction> _program;

			public Computer()
			{
				_accumulator = 0;
				_pc = 0;
				_program = null;
			}

			public void Reset()
			{
				_accumulator = 0;
				_pc = 0;
				foreach (Instruction inst in _program)
					inst.Reset();
			}

			public int RunUntilRepeat()
			{
				while (_program[_pc].ExecutedTimes == 0)
				{
					_program[_pc].ExecutedTimes++;
					switch (_program[_pc].Instuction)
					{
						case "acc":
							Acc(_program[_pc].Value);
							break;
						case "jmp":
							Jmp(_program[_pc].Value);
							break;
						case "nop":
							Nop();
							break;
					}
				}
				return _accumulator;
			}

			public int RunUntilEnd()
			{
				int length = _program.Count();
				while (_pc < length && _program[_pc].ExecutedTimes == 0)
				{
					_program[_pc].ExecutedTimes++;
					switch (_program[_pc].Instuction)
					{
						case "acc":
							Acc(_program[_pc].Value);
							break;
						case "jmp":
							Jmp(_program[_pc].Value);
							break;
						case "nop":
							Nop();
							break;
					}
				}
				if (_pc < length)
					return int.MinValue;

				return _accumulator;
			}

			public void Acc(int value)
			{
				_accumulator += value;
				_pc++;
			}

			public void Jmp(int value)
			{
				_pc += value;
			}

			public void Nop()
			{
				_pc++;
			}

			public void Load(List<string> program)
			{
				_program = new List<Instruction>();
				foreach (string instruction in program)
				{
					Instruction inst = new Instruction
					{
						Instuction = instruction.Split(" ").First(),
						Value = instruction.Split(" ").ElementAt(1).ToInt(),
						ExecutedTimes = 0,
					};
					_program.Add(inst);
				}
			}

			public class Instruction
			{
				public string Instuction { get; set; }
				public int Value { get; set; }
				public int ExecutedTimes { get; set; }
				public void Reset()
				{
					ExecutedTimes = 0;
				}
			}
		}
	}
}
