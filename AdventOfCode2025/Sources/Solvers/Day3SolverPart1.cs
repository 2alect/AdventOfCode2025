using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day3SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		int answer = 0;
		string[] banks = input
			.Split('\n')
			.Select(b => b.Trim())
			.Where(b => !string.IsNullOrEmpty(b))
			.ToArray();

		foreach (var bank in banks)
		{
			answer += BestJoltage(bank);
		}

		Log.Current.LogInformation($"Sum of batteries is: {answer}");

		return answer.ToString();
	}

	private int BestJoltage(string bank)
	{
		int first = bank.Length - 2;
		int second = bank.Length - 1;

		for (int i = first - 1; i >= 0; --i)
		{
			if (bank[i] >= bank[first])
			{
				first = i;
			}
		}

		for (int i = second - 1; i > first; --i)
		{
			if (bank[i] >= bank[second])
			{
				second = i;
			}
		}

		return (bank[first] - '0')*10 + (bank[second] - '0');
	}
}