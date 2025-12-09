using AdventOfCode2025.CoreEntites;

namespace AdventOfCode2025.Utils;

public class DataLoader
{
	private static string InputDirName = "Inputes";
	private static string AnswerDirName = "Answers";
	private static string DayInputPattern = "day{0}.txt";
	private static string DayAnswerPattern = "day{0}part{1}.txt";
	private static string DayUniqueAnswerPattern = "day{0}solver{1}.txt";

	private readonly WebInteractor _webInteractor;
	private readonly string _workingDir;

	public DataLoader(WebInteractor webInteractor, string workingDir)
	{
		_webInteractor = webInteractor;
		_workingDir = workingDir;
	}

	public string Load(int day)
	{
		string inputDir = EnsureDirectory(_workingDir, InputDirName);
		string inputPath = GetInputFilePath(inputDir, day);

		if (File.Exists(inputPath))
		{
			return File.ReadAllText(inputPath);
		}

		string input = _webInteractor.DownloadInput(day);

		SaveToFile(inputPath, input);
		return input;
	}

	public void UpdateAnswer(string answer, int day, Level level)
	{
		string answerDir = EnsureDirectory(_workingDir, AnswerDirName);
		string solutionPath = GetAnswerFilePath(answerDir, day, level);
		SaveToFile(solutionPath, answer);
	}

	public void UpdateUniqueAnswer(string answer, int day, string solverName)
	{
		string answerDir = EnsureDirectory(_workingDir, AnswerDirName);
		string solutionPath = GetAnswerFilePath(answerDir, day, solverName);
		SaveToFile(solutionPath, answer);
	}

	private static string EnsureDirectory(string path, string dirName)
	{
		string dir = Path.Combine(path, dirName);
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		return dir;
	}

	private static string GetInputFilePath(string inputDir, int day)
	{
		string fileName = string.Format(DayInputPattern, day);
		return Path.Combine(inputDir, fileName);
	}

	private static void SaveToFile(string path, string content)
	{
		File.WriteAllText(path, content);
	}

	private static string GetAnswerFilePath(string answerDir, int day, Level level)
	{
		string fileName = string.Format(DayAnswerPattern, day, (int)level);
		return Path.Combine(answerDir, fileName);
	}

	private static string GetAnswerFilePath(string answerDir, int day, string solverName)
	{
		string fileName = string.Format(DayUniqueAnswerPattern, day, solverName);
		return Path.Combine(answerDir, fileName);
	}
}