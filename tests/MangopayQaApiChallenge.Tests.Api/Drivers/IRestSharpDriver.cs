using MangopayQaApiChallenge.Tests.Api.Models;
using RestSharp;

namespace MangopayQaApiChallenge.Tests.Api.Drivers;

public interface IRestSharpDriver
{
    Task<RestResponse> SendPostRequestToTokenizeCardAsync(TokenizeRequestDto tokenizeRequestDto);
}