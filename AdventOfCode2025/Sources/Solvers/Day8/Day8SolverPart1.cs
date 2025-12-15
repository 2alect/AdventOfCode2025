using AdventOfCode2025.Utils;
using AdventOfCode2025.Collections.Data;
using AdventOfCode2025.Solvers.Day8;

namespace AdventOfCode2025.Solvers;

public class Day8SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		Point3Id[] points = Common.ParsePoints(input);

		List<Point3IdPairWithDistance> nearestPoints = Common.CalcNearestPoints(points);
		nearestPoints.Sort((a, b) => a.DistanceSq.CompareTo(b.DistanceSq));

		DisjoinSet<Point3Id> dsu = new();

		foreach (Point3Id point in points)
		{
			dsu.Add(point);
		}

		foreach (Point3IdPairWithDistance connection in nearestPoints.Take(1000))
		{
			dsu.Union(connection.First, connection.Second);
		}

		IEnumerable<int> bigest3 = dsu
			.GetAllGroups()
			.Select(g => g.Value.Count)
			.OrderByDescending(c => c)
			.Take(3);

		long answer = 1;
		foreach (long val in bigest3)
		{
			answer *= val;
		}

		Log.Current.LogInformation($"Sum of three largest circuits sizes is: {answer}");

		return answer.ToString();
	}
}
