using Gateway.Application;
using Gateway.Infrastructure;
using Gateway.Domain;

namespace Gateway.Application;

public class GatewayService: IGatewayService
{
    private readonly IWeatherService _weatherService;
    private readonly INewsService _newsService;

    public GatewayService(IWeatherService weatherService, INewsService newsService)
    {
        _weatherService = weatherService;
        _newsService =  newsService;
    }
    public async Task<Result<DashboardDto>> Get(string zip, string countryCode)
    {
        var weather = await _weatherService.GetWeather(zip, countryCode);
        var news = await _newsService.GetNews(countryCode);

        var result = new DashboardDto
        {
            Weather = weather.Value,
            WeatherError = weather.Error,
            News = news.Value,
            NewsError = news.Error
        };

        return Result<DashboardDto>.Ok(result);
    }
}
