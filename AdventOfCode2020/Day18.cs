using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day18
	{

		public static long Problem1()
		{
			List<string> input = File.ReadAllLines("Data/Day18.txt").ToList();
			long result = 0;
			foreach (string line in input)
				result += CustomMath.Parse(line);

			Console.WriteLine(result);
			return (result);
		}

		public static long Problem2()
		{
			List<string> input = File.ReadAllLines("Data/Day18.txt").ToList();
			long result = 0;

			CustomMath math = new CustomMath();
			foreach (string line in input)
				result += math.ParseUsingStack(line);

			Console.WriteLine(result);
			return (result);
		}
	}

	public class CustomMath
	{
		private Stack<long> operands;
		private Stack<string> operators;

		public static long Parse(string expression)
		{
			List<string> tokenized = expression.Split(" ").ToList();

			int i = 0;
			string lastOperator = "+";
			long result = 0;

			while (i < tokenized.Count)
			{
				long op;
				if (tokenized[i].StartsWith("("))
				{
					string exp = tokenized[i++];
					exp += " " + tokenized[i++];
					while (exp.Count(e => e == '(') != exp.Count(e => e == ')'))
						exp += " " + tokenized[i++];

					op = Parse(exp.Substring(1, exp.Length - 2));
				}
				else
					op = tokenized[i++].ToInt();

				if (lastOperator == "+")
					result += op;
				else if (lastOperator == "*")
					result *= op;
				else
					throw new System.Exception("Error");

				if (i < tokenized.Count)
					lastOperator = tokenized[i++];


			}

			return result;
		}

		public static long ParseAdvanced(string expression)
		{
			List<string> tokenized = expression.Split(" ").ToList();

			int i = 0;
			string lastOperator = "+";
			long result = 0;

			long multplier = 1;
			long adder = 0;

			while (i < tokenized.Count)
			{
				long op;

				if (tokenized[i].StartsWith("("))
				{
					string exp = tokenized[i++];
					exp += " " + tokenized[i++];
					while (exp.Count(e => e == '(') != exp.Count(e => e == ')'))
						exp += " " + tokenized[i++];

					op = Parse(exp.Substring(1, exp.Length - 2));
				}
				else
					op = tokenized[i++].ToInt();

				if (lastOperator == "+")
				{
					adder += op;
				}
				else if (lastOperator == "*")
				{
					result += adder * multplier;
					adder = 0;
					multplier = op;
				}
				else
					throw new System.Exception("Error");

				if (i < tokenized.Count)
					lastOperator = tokenized[i++];

			}

			return result;
		}


		public long ParseUsingStack(string expression)
		{
			operators = new Stack<string>();
			operands = new Stack<long>();

			operators.Push("(");

			int pos = 0;
			while (pos <= expression.Length)
			{

				if (pos == expression.Length || expression[pos] == ')')
				{
					CloseParethesis();
					pos++;
				}
				else if (expression[pos].IsNumber())
				{
					long val = 0;
					while (pos < expression.Length && expression[pos].IsNumber())
					{
						val *= 10;
						val += expression[pos++].ToInt();
					}
					operands.Push(val);
				}
				else if (expression[pos] == ' ')
				{
					pos++;
				}
				else
				{
					ProcessOperator(expression[pos].ToString());
					pos++;
				}
			}


			return operands.Pop();

		}

		public void CloseParethesis()
		{
			while (operators.Peek() != "(")
			{
				ProcessOperation();
			}
			operators.Pop();
		}

		public void ProcessOperation()
		{
			long op2 = operands.Pop();
			long op1 = operands.Pop();

			string op = operators.Pop();

			long result = 0;
			switch (op)
			{
				case "+":
					result = op1 + op2;
					break;
				case "*":
					result = op1 * op2;
					break;
			}

			operands.Push(result);
		}

		public void ProcessOperator(string op)
		{
			while (operators.Count > 0 && Evaluate(op, operators.Peek()))
				ProcessOperation();
			operators.Push(op);
		}


		public bool Evaluate(string op, string prevOp)
		{
			bool evaluate = false;
			switch (op)
			{
				case "*":
					evaluate = (prevOp != "(");
					break;
				case "+":
					evaluate = (prevOp == "+");
					break;
				case ")":
					evaluate = true;
					break;
			}
			return evaluate;
		}


	}

}
