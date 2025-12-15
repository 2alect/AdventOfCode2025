using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public abstract class PointND<T> : IPointND<T>, IEquatable<PointND<T>>
	where T : INumber<T>
{
	public abstract int Dimensions { get; }
	public abstract T this[int i] { get; }

	public static T DistanceSq(IPointND<T> a, IPointND<T> b)
	{
		if (a.Dimensions != b.Dimensions)
			throw new ArgumentException($"Points have different dimensions {a.Dimensions} vs {b.Dimensions}");

		T sum = T.Zero;

		for (int i = 0; i < a.Dimensions; i++)
		{
			T d = a[i] - b[i];
			sum += d * d;
		}

		return sum;
	}

	public static double Distance(IPointND<T> a, IPointND<T> b)
		=> Math.Sqrt(double.CreateChecked(DistanceSq(a, b)));

	public bool Equals(PointND<T>? other)
	{
		if (other is null || other.Dimensions != Dimensions)
			return false;

		for (int i = 0; i < Dimensions; i++)
			if (this[i] != other[i])
				return false;

		return true;
	}

	public override bool Equals(object? obj)
		=> obj is PointND<T> p && Equals(p);

	public override int GetHashCode()
	{
		HashCode hc = new();
		for (int i = 0; i < Dimensions; i++)
			hc.Add(this[i]);

		return hc.ToHashCode();
	}
}