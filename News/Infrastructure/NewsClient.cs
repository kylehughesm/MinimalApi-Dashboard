namespace News.Infrastructure;

public class NewsClient: INewsClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public NewsClient(HttpClient httpClient,IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    private async Task<NewsResponse?> FetchNews(string countryCode)
    {
        var apiKey = _config["NewsApi:ApiKey"];
        var newsUrl = $"https://newsapi.org/v2/top-headlines?country={countryCode}&apiKey={apiKey}";

        var response = await _httpClient.GetAsync(newsUrl);
        var rawJson = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Api Error: {rawJson}");

        var result = await response.Content.ReadFromJsonAsync<NewsResponse>();

        if (result is null)
            throw new Exception("News Api returned empty response");

        return result;
    }

    public async Task<NewsResult> GetNews(string countryCode)
    {
        try
        {
            var raw = await FetchNews(countryCode);
            return MapToResult(raw);
        }
        catch (Exception ex)
        {
            return NewsResult.Fail(ex.Message);
        }
    }

    private NewsResult MapToResult(NewsResponse response)
    {
        if (response.Status != "ok")
            return NewsResult.Fail("News API returned failure");

        var articles = new List<NewsArticleDto>();

        foreach (var article in response.Articles)
        {
            if (string.IsNullOrWhiteSpace(article.Title) || string.IsNullOrWhiteSpace(article.Url))
                continue;

            articles.Add(new NewsArticleDto
            {
                Title = article.Title,
                Summary = article.Description,
                Url = article.Url,
                Source = article.NewsSource.Name,
                PublishedAt = DateTime.Parse(article.Published)
            });
        }

        if (articles.Count == 0)
            return NewsResult.Fail("No news available for the selected country");

        return NewsResult.Ok(articles);
    }
}
