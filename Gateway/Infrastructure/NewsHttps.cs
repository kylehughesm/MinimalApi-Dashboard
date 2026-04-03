using System.Net.Http.Json;
using Gateway.Domain;

namespace Gateway.Infrastructure;

public class NewsService : INewsService
{
    private readonly HttpClient _httpClient;

    public NewsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<NewsArticleDto>>> GetNews(string countryCode)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"?countryCode={Uri.EscapeDataString(countryCode)}");

            var result = await response.Content.ReadFromJsonAsync<NewsResponse>();

            if (!response.IsSuccessStatusCode)
                return Result<List<NewsArticleDto>>.Fail("News service unavailable");

            if (result is null)
                return Result<List<NewsArticleDto>>.Fail("Invalid response from news service");

            if (!result.Success)
                return Result<List<NewsArticleDto>>.Fail(result.Error);

            return Result<List<NewsArticleDto>>.Ok(result.Articles);
        }
        catch
        {
            return Result<List<NewsArticleDto>>.Fail("News service unreachable");
        }
    }
}
