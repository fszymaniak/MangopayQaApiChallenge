using MangopayQaApiChallenge.Tests.Api.Models;
using RestSharp;

namespace MangopayQaApiChallenge.Tests.Api.Drivers;

public interface IRestSharpDriver
{
    Task<RestResponse> SendPostRequestToTokenizeCard(TokenizeRequestDto tokenizeRequestDto);
}