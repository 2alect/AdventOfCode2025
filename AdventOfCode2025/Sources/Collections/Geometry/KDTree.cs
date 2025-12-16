using System.Numerics;

namespace AdventOfCode2025.Collections.Geometry;

public class KDTree<TPoint, T>
	where TPoint : IPointND<T>
	where T : INumber<T>
{
	private readonly KDNode? _root;
	private readonly int _dimensions;
	private readonly IComparer<TPoint>[] _axisComparers;

	public KDTree(TPoint[] points)
	{
		if (points.Length == 0)
			throw new ArgumentException("Points array is empty");

		int dim = _dimensions = points[0].Dimensions;
		_axisComparers = InitComparers(dim);
		_root = Build(points, 0, points.Length - 1, 0);		
	}

	private static IComparer<TPoint>[] InitComparers(int _dimensions)
	{
		IComparer<TPoint>[] axisComparers = new IComparer<TPoint>[_dimensions];
		for (int i = 0; i < _dimensions; i++)
		{
			int axis = i;
			axisComparers[i] = Comparer<TPoint>.Create(
				(a, b) => a[axis].CompareTo(b[axis])
			);
		}

		return axisComparers;
	}

	private KDNode? Build(TPoint[] points, int left, int right, int depth)
	{
		if (left > right)
			return null;

		int axis = depth % _dimensions;
		int mid = (left + right) >> 1;

		Array.Sort(points, left, right - left + 1, _axisComparers[axis]);

		return new KDNode(
			points[mid],
			axis,
			Build(points, left, mid - 1, depth + 1),
			Build(points, mid + 1, right, depth + 1)
		);
	}

	public TPoint Nearest(TPoint target, out T bestDistSq, bool skipExactMatch = false)
	{
		bestDistSq = default!;
		TPoint? best = default;
		bool initialized = false;

		Nearest(_root, target, ref best, ref bestDistSq, ref initialized, skipExactMatch);

		if (!initialized || best is null)
			throw new InvalidOperationException("No nearest point found");

		return best;
	}

	private static void Nearest(
		KDNode? node,
		TPoint target,
		ref TPoint? bestPoint,
		ref T bestDistSq,
		ref bool initialized,
		bool skipExactMatch)
	{
		if (node == null)
			return;

		TPoint cur = node.Point;
		T distSq = PointND<T>.DistanceSq(cur, target);

		if (!initialized && (!skipExactMatch || !cur.Equals(target)))
		{
			bestDistSq = distSq;
			bestPoint = cur;
			initialized = true;
		}
		else if (initialized && distSq < bestDistSq && (!skipExactMatch || !cur.Equals(target)))
		{
			bestDistSq = distSq;
			bestPoint = cur;
		}

		T delta = target[node.Axis] - cur[node.Axis];

		KDNode? first = delta < T.Zero ? node.Left : node.Right;
		KDNode? second = delta < T.Zero ? node.Right : node.Left;

		Nearest(first, target, ref bestPoint, ref bestDistSq, ref initialized, skipExactMatch);

		if (initialized && delta * delta < bestDistSq)
		{
			Nearest(second, target, ref bestPoint, ref bestDistSq, ref initialized, skipExactMatch);
		}
	}

	public bool AnyInRange(TPoint min, TPoint max)
	{
		return AnyInRange(_root, min, max);
	}

	private bool AnyInRange(KDNode? node, TPoint min, TPoint max)
	{
		if (node == null)
			return false;

		var p = node.Point;

		if (Inside(p, min, max))
			return true;

		int axis = node.Axis;
		T value = p[axis];

		if (min[axis] <= value && AnyInRange(node.Left, min, max))
			return true;

		if (max[axis] >= value && AnyInRange(node.Right, min, max))
			return true;

		return false;
	}

	private bool Inside(TPoint p, TPoint min, TPoint max)
	{
		for (int i = 0; i < _dimensions; i++)
		{
			if (p[i] < min[i] || p[i] > max[i])
				return false;
		}

		return true;
	}

	private sealed class KDNode
	{
		public TPoint Point { get; }
		public KDNode? Left { get; }
		public KDNode? Right { get; }
		public int Axis { get; }

		public KDNode(TPoint point, int axis, KDNode? left, KDNode? right)
		{
			Point = point;
			Axis = axis;
			Left = left;
			Right = right;
		}
	}
}
