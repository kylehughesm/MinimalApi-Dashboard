
namespace News.Infrastructure;

public interface INewsClient
{
    public Task<NewsResult> GetNews(string countryCode);
}
