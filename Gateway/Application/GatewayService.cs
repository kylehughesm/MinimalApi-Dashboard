using Gateway.Application;
using Gateway.Infrastructure;
using Gateway.Domain;

namespace Gateway.Application;

public class GatewayService: IGatewayService
{
    private readonly IWeatherService _weatherService;

    public GatewayService(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    public async Task<Result<WeatherDto>> Get()
    {
        var response = await _weatherService.GetWeather();

        return Result<WeatherDto>.Ok(response);
    }
}
