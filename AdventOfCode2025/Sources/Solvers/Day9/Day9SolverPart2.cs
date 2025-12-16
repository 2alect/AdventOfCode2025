using AdventOfCode2025.Collections.Geometry;
using AdventOfCode2025.Utils;
using AdventOfCode2025.Solvers.Day9;
using AdventOfCode2025.Collections.Algorithms;

namespace AdventOfCode2025.Solvers;

public class Day9SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		Point2L[] points = Common.ParsePoints(input);

		(
			Segment2L[] xSortedVertical, 
			long[] sortedX, 
			Segment2L[] ySortedHorizontal, 
			long[] sortedY
		) 
		= BuildSupportCollections(points);

		long answer = 0;

		for (int i = 0; i < points.Length - 1; i++)
		{
			for (int j = i + 1; j < points.Length; j++)
			{
				Point2L a = points[i];
				Point2L b = points[j];

				if (!HasItersections(a, b, xSortedVertical, sortedX, ySortedHorizontal, sortedY))
				{
					answer = Math.Max(answer, Point2L.Area(a, b));
				}
			}
		}

		Log.Current.LogInformation($"Area of the largest rectangle is: {answer}");

		return answer.ToString();
	}

	private static (Segment2L[] xSortedVertical, long[] sortedX, Segment2L[] ySortedHorizontal, long[] sortedY) 
		BuildSupportCollections(Point2L[] points)
	{
		List<Segment2L> horizontalList = new(points.Length - 1);
		List<Segment2L> verticalList = new(points.Length - 1);

		for (int i = 0, j = 1; j < points.Length; i++, j++)
		{
			Point2L a = points[i];
			Point2L b = points[j];

			if (a.X == b.X)
			{
				if (a.Y > b.Y) (a, b) = (b, a);
				verticalList.Add(new Segment2L(a, b));
			}

			if (a.Y == b.Y)
			{
				if (a.X > b.X) (a, b) = (b, a);
				horizontalList.Add(new Segment2L(a, b));
			}
		}

		Segment2L[] xSortedVertical = verticalList.ToArray();
		Array.Sort(xSortedVertical, (a, b) => a.Start.X.CompareTo(b.Start.X));
		long[] sortedX = xSortedVertical.Select(s => s.Start.X).ToArray();

		Segment2L[] ySortedHorizontal = horizontalList.ToArray();
		Array.Sort(ySortedHorizontal, (a, b) => a.Start.Y.CompareTo(b.Start.Y));
		long[] sortedY = ySortedHorizontal.Select(s => s.Start.Y).ToArray();

		return (xSortedVertical, sortedX, ySortedHorizontal, sortedY);
	}

	private static bool HasItersections(Point2L a, Point2L b, Segment2L[] xSortedVertical, long[] sortedX, Segment2L[] ySortedHorizontal, long[] sortedY)
	{
		if (a.X > b.X) (a, b) = (b, a);

		long leftX = a.X;
		long rightX = b.X;

		if (a.Y > b.Y) (a, b) = (b, a);

		long lowY = a.Y;
		long highY = b.Y;

		return IsVerticalIntersect(leftX, rightX, lowY, highY, xSortedVertical, sortedX) 
			|| IsHorizontalIntersect(leftX, rightX, lowY, highY, ySortedHorizontal, sortedY);
	}

	private static bool IsVerticalIntersect(long leftX, long rightX, long lowY, long highY, Segment2L[] xSortedVertical, long[] sortedX)
	{
		int leftInd = BinSearchStart(sortedX, leftX);
		int rightInd = BinSearchEnd(sortedX, rightX);

		for (int i = leftInd; i <= rightInd; i++)
		{
			Segment2L segment = xSortedVertical[i];
			if (((segment.Start.Y > lowY) && (segment.Start.Y < highY)) || ((lowY > segment.Start.Y) && (lowY < segment.End.Y)))
			{
				return true;
			}
		}

		return false;
	}

	private static bool IsHorizontalIntersect(long leftX, long rightX, long lowY, long highY, Segment2L[] ySortedHorizontal, long[] sortedY)
	{
		int lowInd = BinSearchStart(sortedY, lowY);
		int highInd = BinSearchEnd(sortedY, highY);

		for (int i = lowInd; i <= highInd; i++)
		{
			Segment2L segment = ySortedHorizontal[i];
			if (((segment.Start.X > leftX) && (segment.Start.X < rightX)) || ((leftX > segment.Start.X) && (leftX < segment.End.X)))
			{
				return true;
			}
		}

		return false;
	}

	private static int BinSearchStart(long[] sortedArr, long value)
	{
		return BinSearch.LowBoundFirst(sortedArr, value);
	}

	private static int BinSearchEnd(long[] sortedArr, long value)
	{
		return BinSearch.UpperBoundLast(sortedArr, value);
	}
}
