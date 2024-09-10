using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities.PUT;
using MangopayQaApiChallenge.Tests.Api.Models;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class CardFactory : ICardFactory
{
    public CardRegistrationPostDTO CreateValidCard(string userId)
    {
        return new CardRegistrationPostDTO
        (
            userId,
            CurrencyIso.EUR
        );
    }

    public TokenizeRequestDto CreateValidTokenizeRequest(CardRegistrationDTO cardRegistration)
    {
        return new TokenizeRequestDto
        {
            AccessKeyRef = cardRegistration.AccessKey,
            Data = cardRegistration.PreregistrationData,
            CardNumber = TokenizeRequestConstants.CardNumber,
            CardExpirationDate = TokenizeRequestConstants.CardExpirationDate,
            CardCvx = TokenizeRequestConstants.CardCvx,
            Url = cardRegistration.CardRegistrationURL
        };
    }

    public CardRegistrationPutDTO CreateValidCardUpdateRequest(string registrationData)
    {
        return new CardRegistrationPutDTO
        {
            RegistrationData = registrationData
        };
    }
}