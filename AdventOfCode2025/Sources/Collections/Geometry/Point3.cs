using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public class Point3<T> : PointND<T>
	where T : INumber<T>
{
	public T X { get; }
	public T Y { get; }
	public T Z { get; }

	public Point3(T x, T y, T z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public override int Dimensions => 3;

	public override T this[int i] => i switch
	{
		0 => X,
		1 => Y,
		2 => Z,
		_ => throw new IndexOutOfRangeException($"Index {i} is out of range for 3D point")
	};
}

public sealed class Point3L : Point3<long>
{
	public Point3L(long x, long y, long z) : base(x, y, z) {}
}
