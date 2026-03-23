using Weather.Domain;

namespace Weather.Infrastructure;

public interface IGeoClient
{
    public Task<GeoCodeResponse> GetLocation(string zip, string countyrCode);
}
