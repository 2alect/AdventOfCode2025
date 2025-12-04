namespace AdventOfCode2025.Utils;

public static class Log
{
	// Exposed current logger. Default to ConsoleLogger to avoid required wiring.
	private static ILogger _current = new ConsoleLogger();
	public static ILogger Current
	{
		get => _current;
		set => _current = value ?? throw new ArgumentNullException(nameof(value));
	}
}