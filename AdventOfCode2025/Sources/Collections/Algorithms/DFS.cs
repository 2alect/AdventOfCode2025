using AdventOfCode2025.Collections.Data;

namespace AdventOfCode2025.Collections.Algorithms;

internal static class DFS
{
	private const int NOT_CALCULATED = -1;

	public static long CountWays(GraphI graph, int source, int target)
	{
		long[] ways = new long[graph.Links.Count];
		Array.Fill(ways, NOT_CALCULATED);
		return CountWays(graph.Links, source, target, ways);
	}

	private static long CountWays(List<List<int>> links, int cur, int target, long[] ways)
	{
		if (cur == target)
		{
			return 1;
		}

		if (ways[cur] != NOT_CALCULATED)
		{
			return ways[cur];
		}

		long curSumWays = 0;
		foreach (int child in links[cur])
		{
			curSumWays += CountWays(links, child, target, ways);
		}

		ways[cur] = curSumWays;
		return curSumWays;
	}
}
