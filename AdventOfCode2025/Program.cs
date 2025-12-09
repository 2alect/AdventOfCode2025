using AdventOfCode2025.Utils;
using AdventOfCode2025.Solvers.Workflows;

namespace AdventOfCode2025
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Current = new ConsoleLogger();
			ProcessSafe(args);
		}

		private static void ProcessSafe(string[] args)
		{ 			
			try
			{
				Process(args);
			}
			catch (Exception ex)
			{
				Log.Current.LogError($"An error occurred: {ex.Message} at{Environment.NewLine}{ex.StackTrace}");
			}
		}

		private static void Process(string[] args)
		{
			CmdParser cmdParser = new();
			ParsedArgs parsedArgs = cmdParser.ParseArgs(args);
			SDebuggerUtils.Process(parsedArgs.DebugMode);
			new RegularSolveWorkflow().Execute(
				parsedArgs.Day, 
				parsedArgs.Level, 
				parsedArgs.Mode, 
				workingDir: parsedArgs.WorkingDir, 
				sessionCookie: parsedArgs.SessionCookie);
		}
	}
}