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

    public async Task ValidateStatusCode200Ok()
    {
        var lastRequestInfo = await GetLastRequestInfo();
        lastRequestInfo.Response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    public async Task ValidateStatusCode401Unauthorized()
    {
        var lastRequestInfo = await GetLastRequestInfo();
        lastRequestInfo.Response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    public async Task ValidateStatusCode400BadRequest()
    {
        var lastRequestInfo = await GetLastRequestInfo();
        lastRequestInfo.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    private async Task<LastRequestInfo> GetLastRequestInfo() => await Task.Run(() => _api.LastRequestInfo) ?? throw new EmptyLastRequestInfoException();
}