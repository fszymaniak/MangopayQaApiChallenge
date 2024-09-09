using MangoPay.SDK.Core.Enumerations;

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
}