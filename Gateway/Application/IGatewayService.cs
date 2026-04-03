using Gateway.Domain;

namespace Gateway.Application;

public interface IGatewayService
{
    public Task<Result<DashboardDto>> Get(string zip, string countryCode);
}
