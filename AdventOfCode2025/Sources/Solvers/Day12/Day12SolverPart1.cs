using AdventOfCode2025.Utils;
using System.Collections.ObjectModel;

namespace AdventOfCode2025.Solvers;

public class Day12SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		(Shape[] shapes, Region[] regions) = ParseInput(input);

		long answer = 0;

		foreach(Region region in regions)
		{
			int totalFilled = region.Instructions
				.Select((count, i) => shapes[i].FilledArea * count)
				.Sum();

			if (totalFilled < region.Area)
				answer++;
			else 
				Log.Current.LogInformation($"One more NPC problem =( Region: {region.ToString()}");
		}

		Log.Current.LogInformation($"Count of the regions can fit all of the presents listed: {answer}");

		return answer.ToString();
	}

	private static (Shape[] shapes, Region[] regions) ParseInput(string input)
	{
		string[] lines = input.Split('\n')
			.Select(line => line.Trim())
			.ToArray();

		Queue<Shape> shapes = new();
		Queue<string> block = new();

		for (int i = 0; i < lines.Count(); i++)
		{
			string line = lines[i];

			if (line.Any())
			{
				block.Enqueue(line);
				continue;
			}

			if (i < lines.Count() - 1)
			{
				shapes.Enqueue(ParseShape(block));
				block = new();
			}
		}

		Region[] regions = block
			.Select(ParseRegion)
			.ToArray();

		return (shapes.ToArray(), regions);
	}

	private static Shape ParseShape(Queue<string> block)
	{
		string idStr = block.Dequeue();
		int id = int.Parse(idStr.Split(':').First());
		char[][] pattern = block
			.Select(line => line.ToCharArray())
			.ToArray();

		return new Shape(id, pattern);
	}

	private static Region ParseRegion(string line)
	{
		(string areaStr, string instructStr) = line.Split2(':');
		(string widthStr, string heightStr) = areaStr.Split2('x');
		(int width, int height) = (int.Parse(widthStr), int.Parse(heightStr));

		ReadOnlyCollection<int> instructions = instructStr.Split(' ')
			.Where(numStr => numStr.Any())
			.Select(int.Parse)
			.ToList()
			.AsReadOnly();

		return new Region(width, height, instructions);
	}
}

public static class StringExtensions
{
	public static (string a, string b) Split2(this string s, char sep)
	{
		var parts = s.Split(sep, 2);
		return (parts[0], parts[1]);
	}
}

internal class Shape
{
	public const char EMPTY = '.';
	public const char FILLED = '#';

	public int Id { get; private set; }
	public char[][] Pattern { get; private set; }
	public int FilledArea { get; private set; }

	public Shape(int id, char[][] pattern)
	{
		Id = id;
		Pattern = pattern;
		FilledArea = CalcFilled(pattern);
	}

	private static int CalcFilled(char[][] pattern)
	{
		return pattern
			.SelectMany(p => p)
			.Count(x => x == FILLED);
	}
}

internal class Region
{
	public int Width { get; set; }
	public int Height { get; private set; }
	public IReadOnlyList<int> Instructions { get; private set; }

	public Region(int width, int height, IReadOnlyList<int> instructions)
	{
		Width = width;
		Height = height;
		Instructions = instructions;
	}

	public int Area => Width * Height;

	public override string ToString() => $"{Width}x{Height}: {string.Join(' ', Instructions)}";
}
