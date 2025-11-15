using System.Net;
using MangoPay.SDK;
using MangoPay.SDK.Core;
using MangopayQaApiChallenge.Tests.Api.Exceptions;
using Shouldly;

namespace MangopayQaApiChallenge.Tests.Api.Validators;

public class StatusCodeValidator : IStatusCodeValidator
{
    private readonly MangoPayApi _api;
    
    public StatusCodeValidator(MangoPayApi api)
    {
        this._api = api;
    }

    public Task ValidateStatusCodeAsync(HttpStatusCode expectedStatusCode)
    {
        var lastRequestInfo = GetLastRequestInfo();
        lastRequestInfo.Response.StatusCode.ShouldBe(expectedStatusCode);
        return Task.CompletedTask;
    }

    public Task ValidateStatusCode200OkAsync() => ValidateStatusCodeAsync(HttpStatusCode.OK);

    public Task ValidateStatusCode401UnauthorizedAsync() => ValidateStatusCodeAsync(HttpStatusCode.Unauthorized);

    public Task ValidateStatusCode400BadRequestAsync() => ValidateStatusCodeAsync(HttpStatusCode.BadRequest);

    private LastRequestInfo GetLastRequestInfo() => _api.LastRequestInfo ?? throw new EmptyLastRequestInfoException();
}