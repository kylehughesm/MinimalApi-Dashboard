using System.Text.Json.Serialization;

namespace Gateway.Infrastructure;

public class WeatherResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeather Current { get; set; } = new();

    [JsonPropertyName("daily")]
    public DailyWeather Daily { get; set; } = new();
}

public class CurrentWeather
{
    [JsonPropertyName("temperature_2m")]
    public decimal Temperature2m { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("precipitation")]
    public decimal Precipitation { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; } = "";
}

public class DailyWeather
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("temperature_2m_max")]
    public List<decimal> TemperatureMax { get; set; } = new();

    [JsonPropertyName("temperature_2m_min")]
    public List<decimal> TemperatureMin { get; set; } = new();

    [JsonPropertyName("weather_code")]
    public List<int> WeatherCode { get; set; } = new();
}
