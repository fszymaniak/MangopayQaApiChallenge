using MangoPay.SDK.Entities.PUT;
using MangopayQaApiChallenge.Tests.Api.Models;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public interface ICardFactory
{
    public CardRegistrationPostDTO CreateValidCard(string userId);
    
    public TokenizeRequestDto CreateValidTokenizeRequest(CardRegistrationDTO cardRegistration);

    public CardRegistrationPutDTO CreateValidCardUpdateRequest(string registrationData);
}