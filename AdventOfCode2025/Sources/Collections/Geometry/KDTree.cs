using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Collections.Geometry;

public class KDTree<TKey> where TKey : Point3
{
	private readonly KDNode? _root;

	public KDTree(TKey[] points)
	{
		_root = Build(points);
	}

	private KDNode? Build(TKey[] points, int depth = 0)
	{
		if (points.Length == 0)
			return null;

		int axis = depth % 3;

		switch (axis)
		{
			case 0: Array.Sort(points, (a, b) => a.X.CompareTo(b.X)); break;
			case 1: Array.Sort(points, (a, b) => a.Y.CompareTo(b.Y)); break;
			default: Array.Sort(points, (a, b) => a.Z.CompareTo(b.Z)); break;
		}

		int mid = points.Length / 2;

		return new KDNode(points[mid], axis, Build(points[..mid], depth + 1), Build(points[(mid + 1)..], depth + 1));
	}

	public TKey Nearest(TKey target, out long bestDistSq, bool skipExactMatch = false)
	{
		bestDistSq = long.MaxValue;
		TKey? best = default;
		Nearest(_root, target, ref best, ref bestDistSq, skipExactMatch);

		if (best == null)
			throw ExceptionHelper.ThrowException($"No nearest point found for ({target.X}, {target.Y}, {target.Z})");

		return best;
	}

	private static void Nearest(KDNode? curNode, TKey target, ref TKey? bestPoint, ref long bestDistSq, bool skipExactMatch)
	{
		if (curNode == null)
			return;

		TKey curPoint = curNode.Point;
		long distSq = Point3.DistanceSq(curPoint, target);

		if (distSq < bestDistSq && (!skipExactMatch || curPoint != target))
		{
			bestDistSq = distSq;
			bestPoint = curPoint;
		}

		long delta = curNode.Axis switch
		{
			0 => target.X - curPoint.X,
			1 => target.Y - curPoint.Y,
			_ => target.Z - curPoint.Z
		};

		KDNode? first = delta < 0 ? curNode.Left : curNode.Right;
		KDNode? second = delta < 0 ? curNode.Right : curNode.Left;

		Nearest(first, target, ref bestPoint, ref bestDistSq, skipExactMatch);

		if (delta * delta < bestDistSq)
		{
			Nearest(second, target, ref bestPoint, ref bestDistSq, skipExactMatch);
		}
	}

	internal class KDNode
	{
		public TKey Point { get; private set; }
		public KDNode? Left { get; private set; }
		public KDNode? Right { get; private set; }
		public int Axis { get; private set; }

		public KDNode(TKey point, int axis, KDNode? left, KDNode? right)
		{
			Point = point;
			Axis = axis;
			Left = left;
			Right = right;
		}
	}
}

