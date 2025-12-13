namespace AdventOfCode2025.Collections.Geometry;

public class Point2 : IEquatable<Point2>
{
	public long X { get; private set; }
	public long Y { get; private set; }

	public Point2(long x, long y)
	{
		X = x;
		Y = y;
	}

	public static long DistanceSq(Point2 a, Point2 b)
	{
		long dx = a.X - b.X;
		long dy = a.Y - b.Y;
		return dx * dx + dy * dy;
	}

	public static double Distance(Point2 a, Point2 b) => Math.Sqrt(DistanceSq(a, b));

	public static long Area(Point2 a, Point2 b)
	{
		long width = Math.Abs(a.X - b.X) + 1;
		long height = Math.Abs(a.Y - b.Y) + 1;
		return width * height;
	}

	public bool Equals(Point2? other)
	{
		if (other is null) return false;
		return X == other.X && Y == other.Y;
	}

	public override int GetHashCode() => HashCode.Combine(X, Y);
}
