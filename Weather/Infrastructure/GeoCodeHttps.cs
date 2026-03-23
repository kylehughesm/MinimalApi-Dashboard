using Weather.Domain;

namespace Weather.Infrastructure;

public class GeoCodeClient: IGeoClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    
    public GeoCodeClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<GeoCodeResponse> GetLocation(string zip, string countryCode)
    {
        var apiKey = _config["GeoCode:ApiKey"];

        var geoUrl = $"https://api.openweathermap.org/geo/1.0/zip?zip={zip},{countryCode}&appid={apiKey}";

        var response = await _httpClient.GetAsync(geoUrl);

        if (!response.IsSuccessStatusCode)
            throw new Exception("GeoCode API failed.");

        var result =  await response.Content.ReadFromJsonAsync<GeoCodeResponse>();

        if (result is null)
            throw new Exception("GeoCode API returned empty response.");

        return result;
    }
}

