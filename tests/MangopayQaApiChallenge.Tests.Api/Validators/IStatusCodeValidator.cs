namespace MangopayQaApiChallenge.Tests.Api.Validators;

public interface IStatusCodeValidator
{
    Task ValidateStatusCodeAsync(HttpStatusCode expectedStatusCode);

    Task ValidateStatusCode200OkAsync();

    Task ValidateStatusCode401UnauthorizedAsync();

    Task ValidateStatusCode400BadRequestAsync();
}