using MangoPay.SDK;
using Microsoft.Extensions.Configuration;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class TestBaseSetup
{
    protected readonly MangoPayApi Api;
    private readonly AppSettings _appSettings;

    public TestBaseSetup(MangoPayApi api)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false)
            .Build();

        _appSettings = config.Get<AppSettings>()!;
        Api = api;
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        Api.Config.ClientId = _appSettings.ClientId;
        Api.Config.ClientPassword = _appSettings.ClientPassword;
    }
}