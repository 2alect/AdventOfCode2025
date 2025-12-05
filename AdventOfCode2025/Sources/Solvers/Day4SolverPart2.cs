using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day4SolverPart2 : IBaseSolver
{
	const char ROLL = '@';
	const char SIGNED = 'X';
	const char EMPTY = '.';

	public string Solve(string input)
	{
		char[][] grid = input
			.Split('\n')
			.Select(b => b.Trim())
			.Where(b => !string.IsNullOrEmpty(b))
			.Select(b => b.ToArray())
			.ToArray();

		int answer = RemovedRolls(grid);

		Log.Current.LogInformation($"Number of removed rolls: {answer}");

		return answer.ToString();
	}

	private int RemovedRolls(char[][] grid)
	{
		int removed = 0;
		Queue<(int row, int col)> toRemove = new(grid.Length * grid[0].Length);

		for (int row = 0; row < grid.Length; ++row)
		{
			for (int col = 0; col < grid[row].Length; ++col)
			{
				if (IsGoodRoll(grid, row, col))
				{
					toRemove.Enqueue((row, col));
					grid[row][col] = SIGNED;
				}
			}
		}

		while (toRemove.Count > 0)
		{
			var (row, col) = toRemove.Dequeue();
			removed++;
			grid[row][col] = EMPTY;

			for (int dir = 0; dir < DirectX.Count; ++dir)
			{
				int newRow = row + DirectX[dir];
				int newCol = col + DirectY[dir];
				if(IsGoodRoll(grid, newRow, newCol))
				{
					toRemove.Enqueue((newRow, newCol));
					grid[newRow][newCol] = SIGNED;
				}
			}
		}

		return removed;
	}

	private static readonly IReadOnlyList<int> DirectX = [-1, -1, -1, 0, 0, 1, 1, 1];
	private static readonly IReadOnlyList<int> DirectY = [-1, 0, 1, -1, 1, -1, 0, 1];
	private bool IsGoodRoll(char[][] grid, int row, int col)
	{
		if (!IsRoll(grid, row, col))
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