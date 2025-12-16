using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public abstract class SegmentND<TPoint, T>
	where TPoint : PointND<T>
	where T : INumber<T>
{
	public TPoint Start { get; set; }
	public TPoint End { get; set; }

	public SegmentND(TPoint start, TPoint end)
	{
		Start = start;
		End = end;
	}
}
