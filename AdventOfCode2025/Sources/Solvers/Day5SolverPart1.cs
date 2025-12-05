using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day5SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		(List<Interval> intervals, List<long> ingredients) = ParseInput(input);

		List<Interval> sortedMergedIntervals = SortAndMergeIntervals(intervals);
		List<long> sortedStartPoints = new(sortedMergedIntervals.Count);
		sortedStartPoints.AddRange(sortedMergedIntervals.Select(i => i.Start));

		int answer = 0;

		for (int i = 0; i < ingredients.Count; i++)
		{
			answer += IsFresh(sortedMergedIntervals, sortedStartPoints, ingredients[i]) ? 1 : 0;
		}

		Log.Current.LogInformation($"Count of fresh ingredients: {answer}");

		return answer.ToString();
	}

	private static bool IsFresh(List<Interval> sortedMergedIntervals, List<long> sortedStartPoints, long ingredient)
	{
		int pos = sortedStartPoints.BinarySearch(ingredient);

		if (pos >= 0)
		{
			return true;
		}

		pos = ~pos - 1;

		if (pos >= 0 && pos < sortedMergedIntervals.Count)
		{
			Interval interval = sortedMergedIntervals[pos];
			if (ingredient >= interval.Start && ingredient <= interval.End)
			{
				return true;
			}
		}

		return false;
	}

	public record Interval(long Start, long End)
	{
		public static Interval FromStrings(string startStr, string endStr)
		{
			return new Interval(long.Parse(startStr), long.Parse(endStr));
		}
	}

	private (List<Interval> intervals, List<long> ingredients) ParseInput(string input)
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

		// Read ingedients ids
		List<long> ingredients = new(lexemes.Length - intervals.Count - 1);
		string ingredientStr;

		while (enumerator.MoveNext() && !string.IsNullOrEmpty(ingredientStr = enumerator.Current.Trim()))
		{
			long ingredient = long.Parse(ingredientStr);
			ingredients.Add(ingredient);
		}

		return (intervals, ingredients);
	}

	public static List<Interval> SortAndMergeIntervals(List<Interval> intervals)
	{
		intervals.Sort((a, b) => a.Start.CompareTo(b.Start));

		List<Interval> merged = new(intervals.Count);
		Interval current = intervals[0];

		for (int i = 1; i < intervals.Count; i++)
		{
			Interval next = intervals[i];
			if (current.End >= next.Start)
			{
				current = new Interval(current.Start, Math.Max(current.End, next.End));
			}
			else
			{
				merged.Add(current);
				current = next;
			}
		}

		merged.Add(current);

		return merged;
	}

}