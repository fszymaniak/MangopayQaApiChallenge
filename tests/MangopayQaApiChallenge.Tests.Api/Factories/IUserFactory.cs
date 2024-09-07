using MangoPay.SDK.Entities.POST;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public interface IUserFactory
{
    public UserNaturalPayerPostDTO CreateValidUser();
}