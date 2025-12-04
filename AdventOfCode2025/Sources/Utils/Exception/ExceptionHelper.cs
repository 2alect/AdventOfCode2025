namespace AdventOfCode2025.Utils;

public static class ExceptionHelper
{
	public static Exception ThrowException(string message)
	{
		throw new Exception(message);
	}

	public static Exception ThrowException(Exception ex, string message)
	{
		throw new Exception(message, ex);
	}
}