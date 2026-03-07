using Gateway.Application;
using Gateway.Infrastructure;
using Gateway.Domain;

namespace Gateway.Application;

public class GatewayService
{
    private readonly GeoCodeService _geoCodeService;
    private readonly WeatherService _weatherService;

    public GatewayService(GeoCodeService geoCodeService, WeatherService weatherService)
    {
        _geoCodeService = geoCodeService;
        _weatherService = weatherService;
    }
    public async Task<Result<WeatherDto>> Get()
    {
        //pass geocode service parameters to get lat and long

        var geoResponse = await _geoCodeService.GetLocation();

        //pass Weather service parameters to fetch weather

        var weatherResponse = await _weatherService.GetWeather(geoResponse.Lat, geoResponse.Lon);
        

        var dto = new WeatherDto(
                geoResponse.Name,
                weatherResponse.Current.Temperature2m,
                weatherResponse.Current.WeatherCode,
                weatherResponse.Current.Precipitation
            );

        //return Result object with weather 
        return Result<WeatherDto>.Ok(dto);
    }
}
