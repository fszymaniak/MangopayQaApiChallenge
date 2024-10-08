namespace MangopayQaApiChallenge.Tests.Api.Validators;

public interface IStatusCodeValidator
{
    Task ValidateStatusCode200Ok();
    
    Task ValidateStatusCode401Unauthorized();
    
    Task ValidateStatusCode400BadRequest();
}