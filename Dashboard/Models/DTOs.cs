namespace Dashboard.Models;

public class Result<T> 
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public T? Value { get; set; }
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
    public string? Summary { get; set; }
    public string Url { get; set; } = "";
    public string Source { get; set; } = "";
    public DateTime PublishedAt { get; set; }
}

public class WeatherDto
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
