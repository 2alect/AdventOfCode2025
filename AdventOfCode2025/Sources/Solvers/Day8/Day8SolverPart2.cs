using AdventOfCode2025.Utils;
using AdventOfCode2025.Collections.Data;
using AdventOfCode2025.Solvers.Day8;

namespace AdventOfCode2025.Solvers;

public class Day8SolverPart2 : IBaseSolver
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

		bool[] connectedMask = new bool[points.Length];
		int connected = 0;
		long answer = -1;

		foreach (Point3IdPairWithDistance connection in nearestPoints)
		{
			Point3Id a = connection.First;
			Point3Id b = connection.Second;

			dsu.Union(a, b);

			if (!connectedMask[a.Id])
			{
				connectedMask[a.Id] = true;
				connected++;
			}

			if (!connectedMask[b.Id])
			{
				connectedMask[b.Id] = true;
				connected++;
			}

			if (connected == points.Length)
			{
				answer = a.X * b.X;
				break;
			}
		}

		Log.Current.LogInformation($"Product of the X-coord of the last two connected boxes is: {answer}");

		return answer.ToString();
	}
}
