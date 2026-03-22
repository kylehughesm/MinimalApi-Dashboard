using Gateway.Domain;

namespace Gateway.Application;

public interface IGatewayService
{
    public Task<Result<WeatherDto>> Get();
}
