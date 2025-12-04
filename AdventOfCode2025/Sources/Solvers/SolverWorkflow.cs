using AdventOfCode2025.CoreEntites;
using AdventOfCode2025.Utils;

namespace AdventOfCode2025.Solvers;

public class SolverWorkflow
{
	public void Execute(int day, Level level, Mode mode, string workingDir, string sessionCookie)
	{
		IBaseSolver solver = SolverLoader.Load(day, level);
		WebInteractor webInteractor = WebInteractor.CreateAndInit(sessionCookie);
		var dataLoader = new DataLoader(webInteractor, workingDir);
		string input = dataLoader.Load(day);
		string answer = solver.Solve(input);
		dataLoader.UpdateAnswer(answer, day, level);

		Log.Current.LogInformation($"Day {day} - Part {(int)level} Answer: {answer}");

		if (mode == Mode.Submit)
		{
			webInteractor.AutoSubmit(day, level, answer);
		}
	}
}
