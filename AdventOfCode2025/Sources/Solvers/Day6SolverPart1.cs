using System.Numerics;

using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day6SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		List<string> lines = input.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.ToList();

		List<List<int>> numGrid = lines.Take(lines.Count - 1)
			.Select(ParseNumbers)
			.ToList();

		List<char> operators = ParseOperators(lines.Last());

		BigInteger answer = 0;
		for (int col = 0; col < operators.Count; col++)
		{
			BigInteger columnAnswer = numGrid[0][col];
			for (int row = 1; row < numGrid.Count; row++)
			{
				if (operators[col] == '+')
				{
					columnAnswer += numGrid[row][col];
				}
				else if (operators[col] == '*')
				{
					columnAnswer *= numGrid[row][col];
				}
			}

			answer += columnAnswer;
		}

		Log.Current.LogInformation($"Sum of all problems is: {answer}");

		return answer.ToString();
	}

	private List<int> ParseNumbers(string line)
	{
		return line.Split(' ')
			.Select(numStr => numStr.Trim())
			.Where(numStr => numStr.Any())
			.Select(int.Parse)
			.ToList();
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