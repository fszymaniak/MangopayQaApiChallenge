namespace MangopayQaApiChallenge.Tests.Api.Validators;

public class IdValidator : IIdValidator
{
    public void ValidateId(string id, string idPrefix)
    {
        id.ShouldNotBe(null);
        id.ShouldContain(idPrefix);
    }
}