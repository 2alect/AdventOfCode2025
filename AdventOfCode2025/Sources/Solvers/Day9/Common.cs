using AdventOfCode2025.Collections.Geometry;

namespace AdventOfCode2025.Solvers.Day9;

internal static class Common
{
	public static Point2L[] ParsePoints(string input)
	{
		return input.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.Select(Common.PointFromStr)
			.ToArray();
	}

	private static Point2L PointFromStr(string str)
	{
		string[] parts = str.Split(',');
		(int x, int y) = (int.Parse(parts[0]), int.Parse(parts[1]));
		return new Point2L(x, y);
	}
}