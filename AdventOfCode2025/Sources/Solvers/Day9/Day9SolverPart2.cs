using AdventOfCode2025.Collections.Geometry;
using AdventOfCode2025.Utils;
using AdventOfCode2025.Solvers.Day9;

namespace AdventOfCode2025.Solvers;

public class Day9SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		Point2[] points = input.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.Select(Common.PointFromStr)
			.ToArray();

		long answer = 0;

		Log.Current.LogInformation($"Area of the largest rectangle is: {answer}");

		return answer.ToString();
	}
}
