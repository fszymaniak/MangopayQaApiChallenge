using MangopayQaApiChallenge.Tests.Api.Models;
using RestSharp;

namespace MangopayQaApiChallenge.Tests.Api.Drivers;

public class RestSharpDriver : IRestSharpDriver
{
    private readonly IRequestDetailsProvider _requestDetailsProvider = new RequestDetailsProvider();

    public async Task<RestResponse> SendPostRequestToTokenizeCard(TokenizeRequestDto tokenizeRequestDto)
    {
        var client = new RestClient(_requestDetailsProvider.GetUrl(tokenizeRequestDto.Url));
        var request = new RestRequest(_requestDetailsProvider.GetEndpoint(tokenizeRequestDto.Url), Method.Post);

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
        return results;
    }
}