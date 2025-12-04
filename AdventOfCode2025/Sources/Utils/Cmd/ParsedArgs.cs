using AdventOfCode2025.CoreEntites;

namespace AdventOfCode2025.Utils;

public sealed class ParsedArgs
{
	public int Day { get; }
	public Level Level { get; }
	public Mode Mode { get; }
	public string WorkingDir { get; }
	public string SessionCookie { get; }
	public EDebugMode DebugMode { get; }

	public ParsedArgs(int day, Level level, Mode mode, string workingDir, string sessionCookie, EDebugMode debugMode)
	{
		Day = day;
		Level = level;
		Mode = mode;
		WorkingDir = workingDir;
		SessionCookie = sessionCookie;
		DebugMode = debugMode;
	}
}