using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day4SolverPart1 : IBaseSolver
{
	const char ROLL = '@';

	public string Solve(string input)
	{
		char[][] grid = input
			.Split('\n')
			.Select(b => b.Trim())
			.Where(b => !string.IsNullOrEmpty(b))
			.Select(b => b.ToArray())
			.ToArray();

		int answer = GoodRollsCount(grid);

		Log.Current.LogInformation($"Number of accessible rolls: {answer}");

		return answer.ToString();
	}

	private int GoodRollsCount(char[][] grid)
	{
		int answer = 0;

		for (int row = 0; row < grid.Length; ++row)
		{
			for (int col = 0; col < grid[row].Length; ++col)
			{
				if (grid[row][col] == ROLL && IsGoodRoll(grid, row, col))
				{
					++answer;
				}
			}
		}

		return answer;
	}

	private static readonly IReadOnlyList<int> DirectX = [ -1, -1, -1, 0, 0, 1, 1, 1 ];
	private static readonly IReadOnlyList<int> DirectY = [ -1, 0, 1, -1, 1, -1, 0, 1 ];
	private bool IsGoodRoll(char[][] grid, int row, int col)
	{
		if(!IsRoll(grid, row, col))
		{
			return false;
		}

		int neighbours = 0;

		for (int dir = 0; dir < DirectX.Count; ++dir)
		{
			int newRow = row + DirectX[dir];
			int newCol = col + DirectY[dir];
			neighbours += IsRoll(grid, newRow, newCol) ? 1 : 0;
		}

		return neighbours < 4;
	}

	private bool IsRoll(char[][] grid, int row, int col)
	{
		return row >= 0 
			&& row < grid.Length 
			&& col >= 0 
			&& col < grid[row].Length
			&& grid[row][col] == ROLL;
	}
}