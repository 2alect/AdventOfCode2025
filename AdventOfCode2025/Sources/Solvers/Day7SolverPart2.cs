using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class Day7SolverPart2 : IBaseSolver
{
	const char EMPTY = '.';
	const char SPLIT = '^';
	const char ACTIVE_SPLIT = '*';
	const char BEAM = '|';
	const char START = 'S';

	public string Solve(string input)
	{
		char[][] grid = BuildGrid(input);
		long answer = EmulateAndCalcSpilts(grid);

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

	private static long EmulateAndCalcSpilts(char[][] grid)
	{
		(int y, int x) start = FindStart(grid);
		grid[start.y][start.x] = BEAM;
		var beamPower = new long[grid.Length, grid[0].Length];
		beamPower[start.y, start.x] = 1;

		int fromX = start.x - 1;
		int toX = start.x + 1;

		for (int y = start.y + 1; y < grid.Length; y++)
		{
			for (int x = fromX; x <= toX; x++)
			{
				if (grid[y][x] == EMPTY)
				{
					if (grid[y - 1][x] == BEAM)
					{
						grid[y][x] = BEAM;
						beamPower[y, x] += beamPower[y - 1, x]; 
					}

					if (grid[y - 1][x - 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = BEAM;
						beamPower[y, x] += beamPower[y - 1, x - 1];
					}

					if (grid[y - 1][x + 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = BEAM;
						beamPower[y, x] += beamPower[y - 1, x + 1];
					}
				}
				else if (grid[y][x] == SPLIT)
				{
					if (grid[y - 1][x] == BEAM)
					{
						grid[y][x] = ACTIVE_SPLIT;
						beamPower[y, x] += beamPower[y - 1, x];
						fromX = Math.Min(fromX, x - 1);
						toX = Math.Max(toX, x + 1);
					}

					if (grid[y - 1][x - 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = ACTIVE_SPLIT;
						beamPower[y, x] += beamPower[y - 1, x - 1];
						fromX = Math.Min(fromX, x - 1);
						toX = Math.Max(toX, x + 1);
					}

					if (grid[y - 1][x + 1] == ACTIVE_SPLIT)
					{
						grid[y][x] = ACTIVE_SPLIT;
						beamPower[y, x] += beamPower[y - 1, x + 1];
						fromX = Math.Min(fromX, x - 1);
						toX = Math.Max(toX, x + 1);
					}
				}
				else
					throw ExceptionHelper.ThrowException($"Invalid symbol '{grid[y][x]}' at ({y}, {x})");
			}
		}

		long answer = 0;
		int lastY = grid.Length - 1;
		for (int x = fromX; x <= toX; x++)
		{
			answer += beamPower[lastY, x];
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