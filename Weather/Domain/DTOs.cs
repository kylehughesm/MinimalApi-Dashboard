using System.Text.Json.Serialization;

namespace Weather.Domain;

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
    public bool Success { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(bool success, T value, string error)
        => (Success, Value, Error) = (success, value, error);

    public static Result<T> Ok(T value) => new(true, value, null);
    public static Result<T> Fail(string error) => new(false, default, error);
}

public class GeoCodeResponse
{
    public string Zip { get; set; }
    public string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Country { get; set; }
}

public class WeatherResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("current")]
    public Current Current { get; set; } = new();

    [JsonPropertyName("daily")]
    public DailyWeather Daily { get; set; } = new();
}

public class Current
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = "";

    [JsonPropertyName("temperature_2m")]
    public decimal Temperature2m { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("precipitation")]
    public decimal Precipitation { get; set; }
}

public class DailyWeather
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("weather_code")]
    public List<int> WeatherCode { get; set; } = new();

    [JsonPropertyName("temperature_2m_max")]
    public List<decimal> TemperatureMax { get; set; } = new();

    [JsonPropertyName("temperature_2m_min")]
    public List<decimal> TemperatureMin { get; set; } = new();

    [JsonPropertyName("precipitation_sum")]
    public List<decimal> PrecipitationSum {get; set; } = new();
}

