using System.Text.Json.Serialization;

namespace Gateway.Domain;

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
