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
