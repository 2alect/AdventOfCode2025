namespace AdventOfCode2025.Utils;

public static class SDebuggerUtils
{
	public static void Process(EDebugMode mode)
	{
		if (mode == EDebugMode.Off)
		{
			return;
		}

		if (mode == EDebugMode.StartDebugger)
		{
			if (!System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Launch();
			}

			return;
		}

		if (mode == EDebugMode.WaitForAttach)
		{
			while (!System.Diagnostics.Debugger.IsAttached)
			{
				Thread.Sleep(100);
			}

			return;
		}

		throw ExceptionHelper.ThrowException($"Unknown debug mode: {mode}");
	}
}