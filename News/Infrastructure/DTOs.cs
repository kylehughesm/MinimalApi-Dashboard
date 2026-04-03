using System.Text.Json.Serialization;

namespace News.Infrastructure;

public class NewsResponse
{
    [JsonPropertyName("status")]
    public string? Status {get; set;}

    [JsonPropertyName("totalResults")]
    public int TotalResults {get; set;}

    [JsonPropertyName("articles")]
    public List<Article> Articles {get; set;} = new();
}

public class Article
{
    [JsonPropertyName("source")]
    public Source? NewsSource {get; set;}

    [JsonPropertyName("author")]
    public string? Author {get; set;}

    [JsonPropertyName("title")]
    public string? Title {get; set;}

    [JsonPropertyName("description")]
    public string? Description {get; set;}

    [JsonPropertyName("url")]
    public string? Url {get; set;}

    [JsonPropertyName("publishedAt")]
    public string? Published {get; set;}

}

public class Source
{
    [JsonPropertyName("id")]
    public string? ID {get; set;}

    [JsonPropertyName("name")]
    public string? Name {get; set;}
}

public class NewsResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public List<NewsArticleDto>? Articles { get; set; }

    public static NewsResult Ok(List<NewsArticleDto> articles) => new()
    {
        Success = true,
        Articles = articles,
        Error = null
    };

    public static NewsResult Fail(string error) => new()
    {
        Success = false,
        Articles = null,
        Error = error
    };
}

public class NewsArticleDto
{
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Url { get; set; } = "";
    public string Source { get; set; } = "";
    public DateTime PublishedAt { get; set; }
}
