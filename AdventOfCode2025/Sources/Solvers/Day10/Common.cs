using AdventOfCode2025.Utils;

using System.Text.RegularExpressions;

namespace AdventOfCode2025.Solvers.Day10;

public static class Common
{
	public static MachineManual[] ParseMachines(string input)
	{
		return input.Split("\n")
			.Select(line => line.Trim())
			.Where(line => line.Any())
			.Select(ParseMachine)
			.ToArray();
	}

	private static MachineManual ParseMachine(string line)
	{
		var mainRegex = new Regex(@"^\[(?<lights>[.#]+)\]\s+(?<buttons>(\([0-9,]+\)\s*)+)\{(?<weights>[0-9,]+)\}$", RegexOptions.Compiled);

		Match m = mainRegex.Match(line);
		if (!m.Success)
			ExceptionHelper.ThrowException($"Error parse line: {line}");

		// lights
		string lights = m.Groups["lights"].Value;
		int lightsMask = 0;
		for (int i = 0; i < lights.Length; i++)
		{
			if (lights[i] == '#')
				lightsMask |= 1 << i;
		}

		// buttons
		Regex buttonRegex = new Regex(@"\((?<bits>[0-9,]+)\)");
		int[][] buttonMasksBits = buttonRegex.Matches(m.Groups["buttons"].Value)
			.Select(b =>
				b.Groups["bits"].Value
					.Split(',')
					.Select(int.Parse)
					.ToArray()
			)
			.ToArray();

		int[] buttonMasks = new int[buttonMasksBits.Length];
		for (int i = 0; i < buttonMasksBits.Length; i++)
		{
			int mask = 0;
			foreach (int bit in buttonMasksBits[i])
			{
				mask |= 1 << bit;
			}
			buttonMasks[i] = mask;
		}

		return new MachineManual(lightsMask, buttonMasks);
	}
}

public class MachineManual
{
	public int LightsMask { get; set; }
	public int[] ButtonMasks { get; set; }

	public MachineManual(int lightsMask, int[] buttonMasks)
	{
		LightsMask = lightsMask;
		ButtonMasks = buttonMasks;
	}
}