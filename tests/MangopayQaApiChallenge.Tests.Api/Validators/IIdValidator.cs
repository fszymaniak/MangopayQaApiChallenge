using MangoPay.SDK.Entities.GET;

namespace MangopayQaApiChallenge.Tests.Api.Validators;

public interface IIdValidator
{
    void ValidateId(string id, string idPrefix);
}