using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers.Workflows;

public class SeporateSolveWorkflow
{
	public void Execute(int day, string solverName, string workingDir, string sessionCookie)
	{
		Log.Current.LogInformation($"Running day {day} unique solver {solverName}");

		IBaseSolver solver = SolverLoader.Load(solverName);
		WebInteractor webInteractor = WebInteractor.CreateAndInit(sessionCookie);
		var dataLoader = new DataLoader(webInteractor, workingDir);
		string input = dataLoader.Load(day);
		string answer = solver.Solve(input);

		dataLoader.UpdateUniqueAnswer(answer, day, solverName);

		Log.Current.LogInformation($"Answer: {answer}");
	}
}

