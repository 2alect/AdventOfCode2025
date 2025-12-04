using AdventOfCode2025.CoreEntites;
using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public static class SolverLoader
{
	public static IBaseSolver Load(int day, Level level)
	{
		string solverClassName = $"AdventOfCode2025.Solvers.Day{day}SolverPart{(int)level}";

		Type? solverType = Type.GetType(solverClassName);
		if (solverType == null)
		{
			throw ExceptionHelper.ThrowException($"Solver for Day {day} Part {(int)level} not found.");
		}

		if (!typeof(IBaseSolver).IsAssignableFrom(solverType))
		{
			throw ExceptionHelper.ThrowException($"Solver for Day {day} Part {(int)level} does not implement IBaseSolver.");
		}

		return (IBaseSolver)Activator.CreateInstance(solverType)!;
	}
}