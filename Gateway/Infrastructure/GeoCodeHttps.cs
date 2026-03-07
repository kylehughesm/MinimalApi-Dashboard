using Gateway.Domain;

namespace Gateway.Infrastructure;

public class GeoCodeService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    
    public GeoCodeService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<GeoCodeResponse> GetLocation()
    {
        var apiKey = _config["GeoCode:ApiKey"];
        var zipCode = "47546";

        var countryCode = "US";

        var geoUrl = $"https://api.openweathermap.org/geo/1.0/zip?zip={zipCode},{countryCode}&appid={apiKey}";

        return  await _httpClient.GetFromJsonAsync<GeoCodeResponse>(geoUrl);
    }
}
