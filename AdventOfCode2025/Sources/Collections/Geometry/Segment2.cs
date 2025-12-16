using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public abstract class Segment2<T> : SegmentND<Point2<T>, T>
	where T : INumber<T>
{
	public Segment2(Point2<T> start, Point2<T> end)
		: base(start, end) { }
}

public sealed class Segment2L : Segment2<long>
{
	public Segment2L(Point2L start, Point2L end)
		: base(start, end) { }
}