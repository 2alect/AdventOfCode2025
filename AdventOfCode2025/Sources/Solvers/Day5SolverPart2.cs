using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

using Interval = Day5SolverPart1.Interval;

public class Day5SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		List<Interval> intervals = ParseInput(input);

		List<Interval> sortedMergedIntervals = Day5SolverPart1.SortAndMergeIntervals(intervals);
		
		long answer = 0;

		for (int i = 0; i < sortedMergedIntervals.Count; i++)
		{
			Interval current = sortedMergedIntervals[i];
			answer += current.End - current.Start + 1;
		}

		Log.Current.LogInformation($"Count of fresh ingredients: {answer}");

		return answer.ToString();
	}

	private List<Interval> ParseInput(string input)
	{
		string[] lexemes = input.Split('\n');

		// Read intervals
		List<Interval> intervals = new(lexemes.Length);
		IEnumerator<string> enumerator = lexemes.AsEnumerable().GetEnumerator();
		string rangeStr;

		while (enumerator.MoveNext() && !string.IsNullOrEmpty(rangeStr = enumerator.Current.Trim()))
		{
			var rangeIntervalStr = rangeStr.Split('-');
			Interval interval = Interval.FromStrings(rangeIntervalStr[0], rangeIntervalStr[1]);
			intervals.Add(interval);
		}

		return intervals;
	}
}