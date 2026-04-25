using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Dashboard;

public class ApiClient
{
    public HttpClient Http { get; }

    public ApiClient()
    {

        var handler = new CookieHandler
        {
            InnerHandler = new HttpClientHandler()
        };

        Http = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:7057")
        };
    }

       private sealed class CookieHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return base.SendAsync(request, cancellationToken);
        }
    }

}
