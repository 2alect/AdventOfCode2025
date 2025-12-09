using AdventOfCode2025.CoreEntites;
using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers.Workflows;

public class RegularSolveWorkflow
{
	public void Execute(int day, Level level, Mode mode, string workingDir, string sessionCookie)
	{
		Log.Current.LogInformation($"Solving day {day} Level {(int)level}");

		IBaseSolver solver = SolverLoader.Load(day, level);
		WebInteractor webInteractor = WebInteractor.CreateAndInit(sessionCookie);
		var dataLoader = new DataLoader(webInteractor, workingDir);
		string input = dataLoader.Load(day);
		string answer = solver.Solve(input);
		dataLoader.UpdateAnswer(answer, day, level);

		Log.Current.LogInformation($"Answer: {answer}");

		if (mode == Mode.Submit)
		{
			webInteractor.AutoSubmit(day, level, answer);
		}
	}
}
