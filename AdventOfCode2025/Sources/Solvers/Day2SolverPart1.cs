using System.Numerics;

using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day2SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		BigInteger answer = 0;
		var ranges = input.Split(',');
		foreach (var range in ranges)
		{
			answer += SumOfBadNumbers(range);
		}

		Log.Current.LogInformation($"Sum of bad numbers is: {answer}");

		return answer.ToString();
	}

	private BigInteger SumOfBadNumbers(String range)
	{
		var bounds = range.Split('-');
		long lowerBound = long.Parse(bounds[0].Trim());
		long upperBound = long.Parse(bounds[1].Trim());
		BigInteger sum = 0;

		for (long i = lowerBound; i <= upperBound; ++i)
		{
			if (isBad(i.ToString()))
			{
				sum += i;
			}
		}

		return sum;
	}

	private bool isBad(String str)
	{
		str = str.Trim();
		if (str.Length % 2 == 1)
		{
			return false;
		}

		int half = str.Length / 2;
		for (int i = 0; i < half; ++i)
		{
			if (str[i] != str[half + i])
			{
				return false;
			}
		}

		return true;
	}
}