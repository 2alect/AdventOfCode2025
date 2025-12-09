using AdventOfCode2025.CoreEntites;
using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public static class SolverLoader
{
	private const string SolverPattern = "Day{0}SolverPart{1}";

	public static IBaseSolver Load(int day, Level level)
	{
		string solverName = string.Format(SolverPattern, day, (int)level);
		return Load(solverName);
	}

	public static IBaseSolver Load(string solverName)
	{
		string solverClassName = $"AdventOfCode2025.Solvers.{solverName}";
		return LoadInternal(solverClassName);
	}

	private static IBaseSolver LoadInternal(string solverClassName)
	{
		Type? solverType = Type.GetType(solverClassName);
		if (solverType == null)
		{
			throw ExceptionHelper.ThrowException($"Solver {solverClassName} not found.");
		}

		if (!typeof(IBaseSolver).IsAssignableFrom(solverType))
		{
			throw ExceptionHelper.ThrowException($"Solver {solverClassName} does not implement IBaseSolver.");
		}

		return (IBaseSolver)Activator.CreateInstance(solverType)!;
	}
}