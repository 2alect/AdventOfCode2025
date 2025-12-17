using System.Numerics;

namespace AdventOfCode2025.Collections.Algorithms;

internal static class BinSearch
{
	public static int LowBoundFirst<T>(T[] minSortedArr, T value)
		where T : INumber<T>
	{
		int left = 0;
		int right = minSortedArr.Length;

		while (left < right)
		{
			int mid = left + ((right - left) >> 1);

			if (minSortedArr[mid] <= value)
			{
				left = mid + 1;
			}
			else
			{
				right = mid;
			}
		}

		return left;
	}

	public static int UpperBoundLast<T>(T[] minSortedArr, T value)
		where T : INumber<T>
	{
		int left = 0;
		int right = minSortedArr.Length;

		while (left < right)
		{
			int mid = left + ((right - left) >> 1);

			if (minSortedArr[mid] < value)
			{
				left = mid + 1;
			}
			else
			{
				right = mid;
			}
		}

		return left - 1;
	}
}
