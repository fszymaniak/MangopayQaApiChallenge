namespace MangopayQaApiChallenge.Tests.Api.Tools.Providers;

public interface IRequestDetailsProvider
{
    public string GetUrl(string rawUrl);
    
    public string GetEndpoint(string rawUrl);
}