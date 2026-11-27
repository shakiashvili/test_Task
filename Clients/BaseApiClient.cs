using RestSharp;

namespace BookingTests.Clients;

public abstract class BaseApiClient : IDisposable
{
    private const string BaseUrl = "https://restful-booker.herokuapp.com";

    protected RestClient Client { get; }

    protected BaseApiClient()
    {
        Client = new RestClient(BaseUrl);
    }

    protected static RestRequest CreateJsonRequest(string resource, Method method)
    {
        var request = new RestRequest(resource, method);
        request.AddHeader("Accept", "application/json");
        return request;
    }

    public void Dispose()
    {
        Client.Dispose();
        GC.SuppressFinalize(this);
    }
}
