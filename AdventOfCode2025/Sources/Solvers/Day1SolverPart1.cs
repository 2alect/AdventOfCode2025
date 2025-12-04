using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day1SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

		int pos = 50;
		int answer = 0;
		foreach (var line in lines)
		{
			char dir = line[0];
			int dist = int.Parse(line.Substring(1));
			if (dir == 'R')
			{
				pos += dist;
			}
			else if (dir == 'L')
			{
				pos -= dist;
			}
			else
			{
				Log.Current.LogWarning($"Unknown direction: {dir}");
			}

			pos = (pos % 100 + 100) % 100;
			if (pos == 0)
			{
				answer++;
			}
		}
		
		Log.Current.LogInformation($"Number of times position 0 was reached: {answer}");

		return answer.ToString();
	}
}