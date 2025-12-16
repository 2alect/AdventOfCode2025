using AdventOfCode2025.Collections.Geometry;

namespace AdventOfCode2025.Solvers.Day8;

internal static class Common
{
	internal static Point3Id[] ParsePoints(string input)
	{
		int i = 0;
		return input.Split('\n')
					.Where(line => line.Length > 5)
					.Select(line => new Point3Id(i++, Point3FromString(line)))
					.ToArray();
	}

	private static Point3L Point3FromString(string s)
	{
		string[] parts = s.Split(',');
		return new Point3L(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
	}

	internal static List<Point3IdPairWithDistance> CalcNearestPoints(Point3Id[] points)
	{
		int count = points.Length;
		List<Point3IdPairWithDistance> nearestPoints = new(count * (count - 1) / 2);

		for (int i = 0; i < count - 1; i++)
		{
			for (int j = i + 1; j < count; j++)
			{
				Point3Id a = points[i];
				Point3Id b = points[j];
				nearestPoints.Add(new(a, b, Point3L.DistanceSq(a.Point, b.Point)));
			}
		}

		return nearestPoints;
	}
}

internal class Point3Id : IEquatable<Point3Id>
{
	public int Id { get; private set; }
	public Point3L Point { get; private set; }

	public Point3Id(int id, Point3L point)
	{
		Id = id; 
		Point = point; 
	}

	public long X => Point.X;
	public long Y => Point.Y;
	public long Z => Point.Z;

	public bool Equals(Point3Id? other)
	{
		if (other is null) return false;
		return Id == other.Id;
	}
}

internal class Point3IdPairWithDistance
{
	public Point3Id First { get; private set; }
	public Point3Id Second { get; private set; }
	public long DistanceSq { get; private set; }

	public Point3IdPairWithDistance(Point3Id first, Point3Id second, long sqDistance)
	{
		First = first;
		Second = second;
		DistanceSq = sqDistance;
	}
}
