namespace AdventOfCode2025.Collections.Geometry;

public class Point3 : IEquatable<Point3>
{
	public long X { get; private set; }
	public long Y { get; private set; }
	public long Z { get; private set; }

	public Point3(long x, long y, long z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public static long DistanceSq(Point3 a, Point3 b)
	{
		long dx = a.X - b.X;
		long dy = a.Y - b.Y;
		long dz = a.Z - b.Z;
		return dx * dx + dy * dy + dz * dz;
	}

	public bool Equals(Point3? other)
	{
		if (other is null) return false;
		return X == other.X && Y == other.Y && Z == other.Z;
	}

	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
