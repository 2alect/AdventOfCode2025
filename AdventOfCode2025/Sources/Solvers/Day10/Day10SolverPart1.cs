using AdventOfCode2025.Utils;
using AdventOfCode2025.Solvers.Day10;

namespace AdventOfCode2025.Solvers;

public class Day10SolverPart1 : IBaseSolver
{
	public string Solve(string input)
	{
		MachineManual[] manuals = Common.ParseMachines(input);

		long answer = manuals
			.Select(SolveMachine)
			.Sum();

		Log.Current.LogInformation($"The fewest buttons pressed to configure all machines is: {answer}");

		return answer.ToString();
	}

	private static int SolveMachine(MachineManual manual)
	{
		return SolveRecursive(0, manual.LightsMask, manual.ButtonMasks, 0, int.MaxValue, 0);
	}

	private static int SolveRecursive(int curMask, int targetMask, int[] masks, int togglesCount, int minToggles, int curInd)
	{
		if (curMask == targetMask)
		{
			return Math.Min(minToggles, togglesCount);
		}

		if (togglesCount >= minToggles || curInd >= masks.Length)
			return minToggles;

		int solveInclude = SolveRecursive(curMask ^ masks[curInd], targetMask, masks, togglesCount + 1, minToggles, curInd + 1);
		int solveExcluse = SolveRecursive(curMask,					targetMask, masks, togglesCount,		minToggles, curInd + 1);

		return Math.Min(solveInclude, solveExcluse);
	}
}
