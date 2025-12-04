using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day3SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		long answer = 0;
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

	private long BestJoltage(string bank)
	{
		int n = 12;
		var batterie = new int[n];
		for (int i = n - 1, j = bank.Length - 1; i >= 0; --i, --j)
		{
			batterie[i] = j;
		}

		for (int i = 0; i < n; ++i)
		{
			int upTo = (i == 0) ? -1 : batterie[i - 1];

			for (int j = batterie[i] - 1; j > upTo; --j)
			{
				if (bank[j] >= bank[batterie[i]])
				{
					batterie[i] = j;
				}
			}
		}

		long joltage = 0;
		for (int i = 0; i < n; ++i)
		{
			joltage = 10*joltage + (bank[batterie[i]] - '0');
		}

		return joltage;
	}
}