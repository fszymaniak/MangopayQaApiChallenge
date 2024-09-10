namespace MangopayQaApiChallenge.Tests.Api.Tools.Providers;

public class RequestDetailsProvider : IRequestDetailsProvider
{
    public string GetUrl(string rawUrl)
    {
        return rawUrl.Split(".com")[0] + ".com";
    }

    public string GetEndpoint(string rawUrl)
    {
        return rawUrl.Split(".com")[1];
    }
}