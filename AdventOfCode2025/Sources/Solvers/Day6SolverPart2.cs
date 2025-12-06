using System.Numerics;

using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day6SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		(List<List<int>> problems, List<char> operators) = ParseInput(input);

		BigInteger answer = BigInteger.Zero;

		foreach ((List<int> numbers, char op) in problems.Zip(operators))
		{
			answer += CalcColumn(numbers, op);
		}

		Log.Current.LogInformation($"Sum of all problems is: {answer}");

		return answer.ToString();
	}

	private (List<List<int>> problems, List<char> operators) ParseInput(string input)
	{
		List<string> lines = input.Split('\n')
			.Where(line => line.Trim().Any())
			.ToList();

		List<string> numGrid = lines.Take(lines.Count - 1)
			.ToList();

		List<char> operators = ParseOperators(lines.Last());

		return (ParseProblems(numGrid, operators.Count), operators);
	}

	private List<List<int>> ParseProblems(List<string> numGrid, int problemsCount)
	{
		int numbersInProblem = numGrid.Count;
		List<List<int>> problems = new(problemsCount);
		List<int> numbers = new List<int>(numbersInProblem);
		bool prevHasNumber = true;

		for (int col = 0; col < numGrid[0].Length; col++)
		{
			int num = 0;
			bool hasNumber = false;
			for (int row = 0; row < numGrid.Count; row++)
			{
				if (!char.IsDigit(numGrid[row][col])) 
					continue;

				hasNumber = true;
				num = num * 10 + (numGrid[row][col] - '0');
			}

			if (hasNumber)
			{
				if (!prevHasNumber)
				{
					// Start a new problem
					problems.Add(numbers);
					numbers = new List<int>(numbersInProblem);
				}

				numbers.Add(num);
			}

			prevHasNumber = hasNumber;
		}

		problems.Add(numbers);

		return problems;
	}

	private BigInteger CalcColumn(List<int> numbers, char op)
	{
		if (op == '+')
			return CalcSum(numbers);
		else
			return CalcProduct(numbers);
	}

	private BigInteger CalcSum(List<int> numbers)
	{
		BigInteger sum = BigInteger.Zero;

		foreach (int num in numbers)
		{
			sum += num;
		}

		return sum;
	}

	private BigInteger CalcProduct(List<int> numbers)
	{
		BigInteger sum = BigInteger.One;

		foreach (int num in numbers)
		{
			sum *= num;
		}

		return sum;
	}


	private List<char> ParseOperators(string line)
	{
		return line.Split(' ')
			.Select(opStr => opStr.Trim())
			.Where(opStr => opStr.Any())
			.Select(opStr => opStr[0])
			.ToList();
	}
}