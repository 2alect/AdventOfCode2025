using AdventOfCode2025.CoreEntites;

namespace AdventOfCode2025.Utils;

public class WebInteractor
{
	private const string BaseAocUrl = "https://adventofcode.com";
	private const string DayUrlFormat = "/2025/day/{0}";
	private const string InputUrlFormat = $"{DayUrlFormat}/input";
	private const string SubmitUrlFormat = $"{DayUrlFormat}/answer";

	private readonly HttpClient _httpClient;

	public WebInteractor(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public static WebInteractor CreateAndInit(string session)
	{
		var httpClient = new HttpClient()
		{
			BaseAddress = new Uri(BaseAocUrl)
		};

		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyAocClient/1.0)");
		httpClient.DefaultRequestHeaders.Referrer = new Uri(BaseAocUrl);
		httpClient.DefaultRequestHeaders.Add("Cookie", $"session={session}");
		
		return new WebInteractor(httpClient);
	}

	public string DownloadInput(int day)
	{
		Log.Current.LogInformation($"Downloading input for Day {day}...");

		try
		{
			string url = string.Format(InputUrlFormat, day);
			var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

			if (!response.IsSuccessStatusCode)
			{
				throw ExceptionHelper.ThrowException($"Failed to download input. HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
			}
			
			return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
		}
		catch (Exception ex)
		{
			throw ExceptionHelper.ThrowException(ex, "Exception while downloading input.");
		}
	}

	public string SubmitAnswer(int day, Level level, string answer)
	{
		Log.Current.LogInformation($"Submitting answer for part {(int)level}...");

		try
		{
			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("level", ((int)level).ToString()),
				new KeyValuePair<string, string>("answer", answer)
			});

			var url = String.Format(SubmitUrlFormat, day);
			var response = _httpClient.PostAsync(url, content).GetAwaiter().GetResult();
			return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
		}
		catch (Exception ex)
		{
			throw ExceptionHelper.ThrowException(ex, "Exception while submitting answer.");
		}
	}

	public bool IsLevelSolved(int day, Level level)
	{
		try
		{
			var url = String.Format(DayUrlFormat, day);
			var html = _httpClient.GetStringAsync(url).GetAwaiter().GetResult();
			int starCount = CountOccurrences(html, "<span class=\"star\">*</span>");
			return starCount >= (int)level;
		}
		catch (Exception ex)
		{
			throw ExceptionHelper.ThrowException(ex, "Exception while checking if level is solved.");
		}
	}

	private static int CountOccurrences(string text, string pattern)
	{
		int count = 0;
		int index = 0;

		while ((index = text.IndexOf(pattern, index, StringComparison.Ordinal)) != -1)
		{
			count++;
			index += pattern.Length;
		}

		return count;
	}

	public void AutoSubmit(int day, Level level, string answer)
	{
		bool levelSolved = IsLevelSolved(day, level);

		if (levelSolved)
		{
			Log.Current.LogInformation($"Part {(int)level} already solved. Skipping.");
			return;
		}

		var resultHtml = SubmitAnswer(day, level, answer);

		if (resultHtml.Contains("That's the right answer"))
			Log.Current.LogInformation("Correct! *");
		else if (resultHtml.Contains("That's not the right answer"))
			Log.Current.LogInformation("Incorrect.");
		else if (resultHtml.Contains("You gave an answer too recently"))
			Log.Current.LogInformation("Cooldown: wait a minute.");
		else
			Log.Current.LogInformation("Unknown response.");
	}
}