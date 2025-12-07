using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day7SolverPart1 : IBaseSolver
{
	const char EMPTY = '.';
	const char SPLIT = '^';
	const char ACTIVE_SPLIT = '*';
	const char BEAM = '|';
	const char START = 'S';

	public string Solve(string input)
	{
		char[][] grid = BuildGrid(input);
		int answer = EmulateAndCalcSpilts(grid);

		Log.Current.LogInformation($"Count of tachyon splits: {answer}");

		return answer.ToString();
	}

	private static char[][] BuildGrid(string input)
	{
		char[][] grid = ParseCharGrid(input);
		char[][] alignedGrid = new char[grid.Length][];

		for (int y = 0; y < grid.Length; y++)
		{
			alignedGrid[y] = new char[grid[y].Length + 2];
			alignedGrid[y][0] = EMPTY;
			Array.Copy(grid[y], 0, alignedGrid[y], 1, grid[y].Length);
			alignedGrid[y][grid[y].Length + 1] = EMPTY;
		}

		return alignedGrid;
	}

	private static char[][] ParseCharGrid(string input)
	{
		return input.Split('\n')
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.Select(line => line.ToArray())
			.ToArray();
	}

	private static int EmulateAndCalcSpilts(char[][] grid)
	{
		(int y, int x) start = FindStart(grid);
		grid[start.y][start.x] = BEAM;

		int fromX = start.x - 1;
		int toX = start.x + 1;
		int answer = 0;

		for (int y = start.y + 1; y < grid.Length; y++)
		{
			for (int x = fromX; x <= toX; x++)
			{
				if (grid[y][x] == EMPTY)
				{
					if (grid[y - 1][x] == BEAM || grid[y - 1][x - 1] == ACTIVE_SPLIT || grid[y - 1][x + 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = BEAM;
					}
				}
				else if (grid[y][x] == SPLIT)
				{
					if (grid[y - 1][x] == BEAM || grid[y - 1][x - 1] == ACTIVE_SPLIT || grid[y - 1][x + 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = ACTIVE_SPLIT;
						answer++;
						fromX = Math.Min(fromX, x - 1);
						toX = Math.Max(toX, x + 1);
					}
				}
				else
					throw ExceptionHelper.ThrowException($"Invalid symbol '{grid[y][x]}' at ({y}, {x})");
			}
		}

		return answer;
	}

	private static (int y, int x) FindStart(char[][] grid)
	{
		for (int x = 0; x < grid.Length; x++)
		{
			if (grid[0][x] == START)
			{
				return (0, x);
			}
		}

		throw ExceptionHelper.ThrowException("Start position not found in the grid.");
	}
}