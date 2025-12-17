using AdventOfCode2025.Utils;
using AdventOfCode2025.Collections.Data;
using AdventOfCode2025.Collections.Algorithms;
using AdventOfCode2025.Solvers.Day11;

namespace AdventOfCode2025.Solvers;

public class Day11SolverPart2 : IBaseSolver
{
	public string Solve(string input)
	{
		(GraphI graph, Dictionary<string, int> names) = Common.ReadGraphWithNames(input);

		int src = names["svr"];
		int dac = names["dac"];
		int fft = names["fft"];
		int trg = names["out"];

		long src2dac = DFS.CountWays(graph, src, dac);
		long dac2fft = DFS.CountWays(graph, dac, fft);
		long fft2trg = DFS.CountWays(graph, fft, trg);

		long src2fft = DFS.CountWays(graph, src, fft);
		long fft2dac = DFS.CountWays(graph, fft, dac);
		long dac2trg = DFS.CountWays(graph, dac, trg);

		long answer = src2dac * dac2fft * fft2trg + src2fft * fft2dac * dac2trg;

		Log.Current.LogInformation($"Count of ways over dac and fft is: {answer}");

		return answer.ToString();
	}
}
