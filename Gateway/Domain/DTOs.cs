namespace Gateway.Domain;

public record WeatherDto
(
    string Name,
    CurrentWeather Current,
    List<DailyForecast> Forecast
);

public record CurrentWeather
(
    DateTime Time,
    decimal Temp,
    string Description,
    decimal PrecipitationSum
);

public record DailyForecast
(
    DateTime Day,
    string Description, 
    decimal MaxTemp,
    decimal MinTemp,
    decimal PrecipitationSum
);

public sealed class Result<T>
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public T? Value { get; set; }

    public static Result<T> Ok(T value) => new()
    {
        Success = true,
        Value = value,
        Error = null
    };

    public static Result<T> Fail(string error) => new()
    {
        Success = false,
        Value = default,
        Error = error
    };
}

public class NewsResponse
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public List<NewsArticleDto>? Articles { get; set; }
}

public class DashboardDto
{
    public WeatherDto? Weather { get; set; }
    public string? WeatherError { get; set; }

    public List<NewsArticleDto>? News { get; set; }
    public string? NewsError { get; set; }
}

public class NewsArticleDto
{
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Url { get; set; } = "";
    public string Source { get; set; } = "";
    public DateTime PublishedAt { get; set; }
}

public record UpdatePreferencesRequest(string Zip, string CountryCode, string TemperatureUnit);