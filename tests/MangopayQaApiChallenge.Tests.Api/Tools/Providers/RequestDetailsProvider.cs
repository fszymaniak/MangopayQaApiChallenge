namespace MangopayQaApiChallenge.Tests.Api.Tools.Providers;

public class RequestDetailsProvider : IRequestDetailsProvider
{
    public string GetUrl(string rawUrl)
    {
        return rawUrl.Split(UrlConstants.DomainSuffix)[UrlConstants.UrlDomainPartIndex] + UrlConstants.DomainSuffix;
    }

    public string GetEndpoint(string rawUrl)
    {
        return rawUrl.Split(UrlConstants.DomainSuffix)[UrlConstants.UrlEndpointPartIndex];
    }
}