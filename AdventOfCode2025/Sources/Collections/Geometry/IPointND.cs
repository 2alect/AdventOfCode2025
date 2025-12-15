using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public interface IPointND<T> where T : INumber<T>
{
	int Dimensions { get; }
	T this[int i] { get; }
}
