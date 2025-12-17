using AdventOfCode2025.Collections.Data;

namespace AdventOfCode2025.Solvers.Day11;

internal static class Common
{
	public static (GraphI graph, Dictionary<string, int>) ReadGraphWithNames(string input)
	{
		Dictionary<string, List<string>> strGraph = ReadStrGraph(input);
		Dictionary<string, int> names = new(strGraph.Count);

		foreach (string name in strGraph.Keys)
		{
			RegisterName(names, name, out _);
		}

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
				if (RegisterName(names, destStr, out int ind))
					links.Add(new List<int>());

				dests.Add(ind);
			}
		}

		return (new GraphI(links), names);
	}

	private static Dictionary<string, List<string>> ReadStrGraph(string input)
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

	private static bool RegisterName(Dictionary<string, int> names, string name, out int index)
	{
		index = -1;

		if (!names.TryGetValue(name, out index))
		{
			index = names.Count;
			names[name] = index;
			return true;
		}

		return false;
	}
}