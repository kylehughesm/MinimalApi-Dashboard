namespace Dashboard.Models;

public class WeatherResponseDto
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public WeatherValueDto? Value { get; set; }
}

public class WeatherValueDto
{
    public string Name { get; set; } = "";
    public CurrentWeatherDto? Current { get; set; }
    public List<DailyForecastDto> Forecast { get; set; } = [];
}

public class CurrentWeatherDto
{
    public DateTime Time { get; set; }
    public double Temp { get; set; }
    public string Description { get; set; } = "";
    public double PrecipitationSum { get; set; }
}

public class DailyForecastDto
{
    public DateTime Day { get; set; }
    public string Description { get; set; } = "";
    public double MaxTemp { get; set; }
    public double MinTemp { get; set; }
    public double PrecipitationSum { get; set; }
}

public class CountryOption
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
}
