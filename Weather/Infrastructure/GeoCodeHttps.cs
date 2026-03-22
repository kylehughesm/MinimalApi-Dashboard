using Weather.Domain;

namespace Weather.Infrastructure;

public class GeoCodeClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    
    public GeoCodeClient(HttpClient httpClient, IConfiguration config)
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

        var response = await _httpClient.GetAsync(geoUrl);

        if (!response.IsSuccessStatusCode)
            throw new Exception("GeoCode API failed.");

        var result =  await response.Content.ReadFromJsonAsync<GeoCodeResponse>();

        if (result is null)
            throw new Exception("GeoCode API returned empty response.");

        return result;
    }
}

