namespace MangopayQaApiChallenge.Tests.Api.Factories;

public interface ICardFactory
{
    public CardRegistrationPostDTO CreateValidCard(string userId);
}