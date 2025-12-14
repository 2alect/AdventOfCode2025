using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day11SolverPart1 : IBaseSolver
{
	private const int NOT_CALCULATED = -1;

	public string Solve(string input)
	{
		Graph graph = ReadGraph(input);

		long[] ways = new long[graph.Names.Count];
		Array.Fill(ways, NOT_CALCULATED);
		long answer = DfsCountWays(graph.Links, graph.Names["you"], graph.Names["out"], ways);

		Log.Current.LogInformation($"Count of ways is: {answer}");

		return answer.ToString();
	}

	public static long DfsCountWays(List<List<int>> graph, int cur, int target, long[] ways)
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
		foreach (int child in graph[cur])
		{
			curSumWays += DfsCountWays(graph, child, target, ways);
		}

		ways[cur] = curSumWays;
		return curSumWays;
	}

	public static Graph ReadGraph(string input)
	{
		Dictionary<string, List<string>> strGraph = ReadStrGraph(input);
		Dictionary<string, int> names = new(strGraph.Count);

		foreach (string name in strGraph.Keys)
		{
			RegisterName(names, name);
		};

		List<List<int>> links = new(names.Count);
		for (int i = 0; i < names.Count; i++)
		{
			links.Add(new List<int>());
		}

		foreach ((string fromStr, List<string> destsStr) in strGraph)
		{
			int src = names[fromStr];
			List<int> dests = links[src];
			dests.EnsureCapacity(destsStr.Count);

			foreach (string destStr in destsStr)
			{
				dests.Add(RegisterName(names, destStr));
			}

			links.Add(dests);
		}

		return new Graph(links, names);
	}

	public static Dictionary<string, List<string>> ReadStrGraph(string input)
	{
		List<string> lines = input
			.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.ToList();

		Dictionary<string, List<string>> graph = new(lines.Count);

		foreach (string line in lines)
		{
			string[] parts = line.Split(':');
			string name = parts[0].Trim();
			List<string> connections = parts[1]
				.Split(' ')
				.Select(dst => dst.Trim())
				.Where(dst => dst.Any())
				.ToList();

			graph.Add(name, connections);
		}

		return graph;
	}

	private static int RegisterName(Dictionary<string, int> names, string name)
	{
		int index = -1;

		if (!names.TryGetValue(name, out index))
		{
			index = names.Count;
			names[name] = index;
		}

		return index;
	}
}

public class Graph
{
	public List<List<int>> Links { get; private set; }
	public Dictionary<string, int> Names { get; private set; }

	public Graph(List<List<int>> links, Dictionary<string, int> names)
	{
		Links = links;
		Names = names;
	}
}
