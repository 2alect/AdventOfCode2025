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

	private static Point3 Point3FromString(string s)
	{
		string[] parts = s.Split(',');
		return new Point3(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
	}

	internal static List<Point3IdPairWithDistance> CalcNearestPoints(Point3Id[] points)
	{
		int count = points.Length;
		List<Point3IdPairWithDistance> nearestPoints = new(count * (count - 1) / 2);

		for (int i = 0; i < count - 1; i++)
		{
			for (int j = i + 1; j < count; j++)
			{
				Point3Id a = new(i, points[i]);
				Point3Id b = new(j, points[j]);
				nearestPoints.Add(new(a, b, Point3.DistanceSq(a, b)));
			}
		}

		return nearestPoints;
	}
}

internal class Point3Id : Point3, IEquatable<Point3Id>
{
	public int Id { get; private set; }

	public Point3Id(int id, Point3 point) : this(id, point.X, point.Y, point.Z) { }

	public Point3Id(int id, long x, long y, long z) : base(x, y, z)
	{
		Id = id;
	}

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
