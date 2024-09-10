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
        
        request.AddHeader("Content-Type","application/x-www-form-urlencoded");
        
        request.AddParameter("accessKeyRef",tokenizeRequestDto.AccessKeyRef, ParameterType.GetOrPost);
        request.AddParameter("data",tokenizeRequestDto.Data, ParameterType.GetOrPost);
        request.AddParameter("cardNumber",tokenizeRequestDto.CardNumber, ParameterType.GetOrPost);
        request.AddParameter("cardExpirationDate",tokenizeRequestDto.CardExpirationDate, ParameterType.GetOrPost);
        request.AddParameter("cardCvx",tokenizeRequestDto.CardCvx, ParameterType.GetOrPost);
        
        var results = await client.ExecuteAsync(request);
        return results;
    }
}