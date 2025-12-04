using System.CommandLine;

using AdventOfCode2025.CoreEntites;

namespace AdventOfCode2025.Utils;

public class CmdParser
{
	public ParsedArgs ParseArgs(string[] args)
	{
		var dayOption = new Option<int>("-d", "--day") { Description = "Solver day", DefaultValueFactory = parseResult => 1 };
		var levelOption = new Option<int>("-l", "--level") { Description = "Solver level (1 or 2)", DefaultValueFactory = parseResult => 1 };
		var submitOption = new Option<bool>("-s", "--submit") { Description = "Submit answer automatically" };
		var workingDirPathOption = new Option<string>("-p", "--working-dir-path") { Description = $"Path to working directory, used to save data, current directory for default", DefaultValueFactory = ParseResult => string.Empty };
		var sessionCookieOption = new Option<string>("-c", "--session-cookie") { Description = $"AoC session authentification cookie (could be found in developer console of Web Browser)", Required = true };
		var debugOption = new Option<bool>("-g", "--debug") { Description = "Start debugger, show attach window" };
		var waitDebuggerOption = new Option<bool>("-w", "--wait-debuger") { Description = "Wait for debugger attach" };

		var root = new RootCommand
		{
			dayOption,
			levelOption,
			submitOption,
			workingDirPathOption,
			sessionCookieOption,
			debugOption,
			waitDebuggerOption
		};

		var parseResult = root.Parse(args ?? Array.Empty<string>());

		int day = parseResult.GetValue(dayOption);
		if (day < 1 || day > 25)
		{
			throw ExceptionHelper.ThrowException($"Day value must be between 1 and 25. Actual value: {day}");
		}

		int levelInt = parseResult.GetValue(levelOption);
		Level level = (Level)levelInt;
		if (levelInt < 1 || levelInt > 2)
		{
			throw ExceptionHelper.ThrowException($"Level value must be 1 or 2. Actual value: {levelInt}");
		}

		bool submit = parseResult.GetValue(submitOption);
		Mode submitMode = submit ? Mode.Submit : Mode.Test;

		string? workingDirPath = parseResult.GetValue(workingDirPathOption);
		if (string.IsNullOrWhiteSpace(workingDirPath))
		{
			workingDirPath = Directory.GetCurrentDirectory();
		}

		string? sessionCookie = parseResult.GetValue(sessionCookieOption);
		if (string.IsNullOrWhiteSpace(sessionCookie))
		{
			throw ExceptionHelper.ThrowException("Session cookie cannot be empty.");
		}

		bool debug = parseResult.GetValue(debugOption);
		bool waitDebugger = parseResult.GetValue(waitDebuggerOption);
		var debugMode = EDebugMode.Off;

		if (debug && waitDebugger)
		{
			throw ExceptionHelper.ThrowException("Cannot use both --debug and --wait-debuger options at the same time.");
		}

		if (debug)
		{
			debugMode = EDebugMode.StartDebugger;
		} 
		else if (waitDebugger)
		{
			debugMode = EDebugMode.WaitForAttach;
		}

		return new ParsedArgs(day, level, submitMode, workingDir: workingDirPath!, sessionCookie: sessionCookie!, debugMode);
	}
}