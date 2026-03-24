namespace Dashboard;

public class ApiClient
{
    public HttpClient Http { get; }

    public ApiClient()
    {
        Http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:7057")
        };
    }
}
