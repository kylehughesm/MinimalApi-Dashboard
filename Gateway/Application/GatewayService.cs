using Gateway.Application;
using Gateway.Infrastructure;
using Gateway.Domain;

namespace Gateway.Application;

public class GatewayService
{
    private readonly WeatherService _weatherService;

    public GatewayService(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    public async Task<Result<WeatherDto>> Get()
    {
        var response = await _weatherService.GetWeather();

        return Result<WeatherDto>.Ok(response);
    }
}
