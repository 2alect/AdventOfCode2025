using AdventOfCode2025.Utils;
using AdventOfCode2025.Collections.Data;
using AdventOfCode2025.Collections.Algorithms;
using AdventOfCode2025.Solvers.Day11;

namespace AdventOfCode2025.Solvers;

public class Day11SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		(GraphI graph, Dictionary<string, int> names) = Common.ReadGraphWithNames(input);

		long answer = DFS.CountWays(graph, names["you"], names["out"]);

		Log.Current.LogInformation($"Count of ways is: {answer}");

		return answer.ToString();
	}
}
