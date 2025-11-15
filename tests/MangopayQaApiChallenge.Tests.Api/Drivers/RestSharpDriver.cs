using MangopayQaApiChallenge.Tests.Api.Logging;
using MangopayQaApiChallenge.Tests.Api.Models;
using RestSharp;

namespace MangopayQaApiChallenge.Tests.Api.Drivers;

public class RestSharpDriver : IRestSharpDriver
{
    private readonly IRequestDetailsProvider _requestDetailsProvider = new RequestDetailsProvider();

    public async Task<RestResponse> SendPostRequestToTokenizeCardAsync(TokenizeRequestDto tokenizeRequestDto)
    {
        var url = _requestDetailsProvider.GetUrl(tokenizeRequestDto.Url);
        var endpoint = _requestDetailsProvider.GetEndpoint(tokenizeRequestDto.Url);

        TestLogger.ApiRequest("POST", $"{url}{endpoint}");

        var client = new RestClient(url);
        var request = new RestRequest(endpoint, Method.Post);

        request.AddHeader(HeaderConstants.Name, HeaderConstants.XwwwFormUrlencodedValue);

        request.AddParameter(RequestNameParametersConstants.AccessKeyRef, tokenizeRequestDto.AccessKeyRef,
            ParameterType.GetOrPost);
        request.AddParameter(RequestNameParametersConstants.Data, tokenizeRequestDto.Data, ParameterType.GetOrPost);
        request.AddParameter(RequestNameParametersConstants.CardNumber, tokenizeRequestDto.CardNumber,
            ParameterType.GetOrPost);
        request.AddParameter(RequestNameParametersConstants.CardExpirationDate, tokenizeRequestDto.CardExpirationDate,
            ParameterType.GetOrPost);
        request.AddParameter(RequestNameParametersConstants.CardCvx, tokenizeRequestDto.CardCvx,
            ParameterType.GetOrPost);

        var results = await client.ExecuteAsync(request);

        TestLogger.ApiResponse("POST", $"{url}{endpoint}", (int)results.StatusCode);

        return results;
    }
}