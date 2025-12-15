using AdventOfCode2025.Collections.Geometry;
using AdventOfCode2025.Utils;
using AdventOfCode2025.Solvers.Day9;

namespace AdventOfCode2025.Solvers;

public class Day9SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		Point2L[] points = input.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.Select(Common.PointFromStr)
			.ToArray();

		long answer = 0;

		for (int i = 0; i < points.Length - 1; i++)
		{
			for (int j = i + 1; j < points.Length; j++)
			{
				Point2L a = points[i];
				Point2L b = points[j];
				answer = Math.Max(answer, Point2L.Area(a, b));
			}
		}

		Log.Current.LogInformation($"Area of the largest rectangle is: {answer}");

		return answer.ToString();
	}
}
