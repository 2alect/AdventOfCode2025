using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public class Point2<T> : PointND<T>
	where T : INumber<T>
{
	public T X { get; }
	public T Y { get; }

	public Point2(T x, T y)
	{
		X = x;
		Y = y;
	}

	public override int Dimensions => 2;

	public override T this[int i] => i switch
	{
		0 => X,
		1 => Y,
		_ => throw new IndexOutOfRangeException($"Index {i} is out of range for 2D point")
	};

	public static T Area(Point2<T> a, Point2<T> b)
	{
		T width = T.Abs(a.X - b.X) + T.One;
		T height = T.Abs(a.Y - b.Y) + T.One;
		return width * height;
	}
}

public sealed class Point2L : Point2<long>
{
	public Point2L(long x, long y) : base(x, y) { }
}
