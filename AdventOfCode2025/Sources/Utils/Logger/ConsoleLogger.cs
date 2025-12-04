namespace AdventOfCode2025.Utils;

public class ConsoleLogger : ILogger
{
	public void LogInformation(string message)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"[INFO] {message}");
		Console.ResetColor();
	}
	public void LogWarning(string message)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine($"[WARN] {message}");
		Console.ResetColor();
	}
	public void LogError(string message)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"[ERROR] {message}");
		Console.ResetColor();
	}
	public void LogDebug(string message)
	{
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine($"[DEBUG] {message}");
		Console.ResetColor();
	}
}