using System.Numerics;

using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day2SolverPart2 : IBaseSolver
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

	private BigInteger SumOfBadNumbers(string range)
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

	private bool isBad(string str)
	{
		str = str.Trim();

		if (str.Length == 1)
			return false;

		int half = str.Length / 2;

		for (int numCount = 1; numCount <= half; ++numCount)
		{
			if (IsBad(str, numCount))
			{
				return true;
			}
		}

		return false;
	}

	private bool IsBad(string str, int numCount)
	{
		if (str.Length % numCount != 0)
		{
			return false;
		}

		int partCount = str.Length / numCount;
		for (int i = 0; i < numCount; ++i)
		{
			char c = str[i];
			for (int j = 0; j < partCount; ++j)
			{
				if (str[j * numCount + i] != c)
				{
					return false;
				}
			}
		}

		return true;
	}
}