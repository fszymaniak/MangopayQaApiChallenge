using RestSharp;

namespace MangopayQaApiChallenge.Tests.Api.Steps;

public class CardSteps
{
    public CardSteps()
    {
    }

    public async Task<CardRegistrationDTO> RegisterCardViaPostApiCall(string userId, ICardFactory cardFactory, MangoPayApi api)
    {
        var cardRegistrationRequest = cardFactory.CreateValidCard(userId);
        var cardRegistrationResponse = await api.CardRegistrations.CreateAsync(cardRegistrationRequest);
        return cardRegistrationResponse;
    }

    public async Task<RestResponse?> TokenizeCardViaPostApiCall(CardRegistrationDTO cardRegistrationResponse, ICardFactory cardFactory, IRestSharpDriver restSharpDriver)
    {
        var tokenizeRequest = cardFactory.CreateValidTokenizeRequest(cardRegistrationResponse);
        var tokenizeResponse =  await restSharpDriver.SendPostRequestToTokenizeCard(tokenizeRequest);
        return tokenizeResponse;
    }
    
    public async Task<CardRegistrationDTO> UpdateRegisteredCardViaPutApiCall(string registrationData, string cardId, ICardFactory cardFactory, MangoPayApi api)
    {
        var cardRegistrationUpdateRequest = cardFactory.CreateValidCardUpdateRequest(registrationData);
        var cardRegistrationUpdateResponse = await api.CardRegistrations.UpdateAsync(cardRegistrationUpdateRequest, cardId);
        return cardRegistrationUpdateResponse;
    }
}