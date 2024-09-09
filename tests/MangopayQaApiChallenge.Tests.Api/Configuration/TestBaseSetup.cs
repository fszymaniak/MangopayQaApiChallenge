﻿

using Allure.Net.Commons;
using NUnit.Framework.Interfaces;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

[AllureNUnit]
public class TestBaseSetup
{
    protected readonly MangoPayApi Api;
    protected readonly IUserFactory UserFactory;
    protected readonly IWalletFactory WalletFactory;
    protected readonly ICardFactory CardFactory;
    protected readonly IStatusCodeValidator StatusCodeValidator;
    protected readonly IIdValidator IdValidator;
    private readonly IPathProvider _pathProvider;
    private readonly IUserValuesRandomizer _userValuesRandomizer;
    private readonly AppSettings _appSettings;
    
    public TestBaseSetup(MangoPayApi api)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false)
            .AddUserSecrets<TestBaseSetup>()
            .Build();

        _appSettings = config.Get<AppSettings>()!;
        Api = api;
        _pathProvider = new PathProvider();
        _userValuesRandomizer = new UserValuesRandomizer();
        UserFactory = new UserFactory(_pathProvider, _userValuesRandomizer);
        WalletFactory = new WalletFactory();
        CardFactory = new CardFactory();
        StatusCodeValidator = new StatusCodeValidator(Api);
        IdValidator = new IdValidator();

    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        Api.Config.ClientId = _appSettings.ClientId;
        Api.Config.ClientPassword = _appSettings.ClientPassword;
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
        {
            // here you can add your screen shot
            // AllureApi.AddScreenDiff("expected.png", "actual.png", "diff.png");
        }
    }
}