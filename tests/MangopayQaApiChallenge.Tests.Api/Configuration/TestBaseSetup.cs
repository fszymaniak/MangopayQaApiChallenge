using Allure.NUnit;
using Microsoft.Extensions.Configuration;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class TestBaseSetup
{
    protected readonly MangoPayApi Api;
    protected readonly IUserFactory UserFactory;
    protected readonly IWalletFactory WalletFactory;
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
        StatusCodeValidator = new StatusCodeValidator(Api);
        IdValidator = new IdValidator();

    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        Api.Config.ClientId = _appSettings.ClientId;
        Api.Config.ClientPassword = _appSettings.ClientPassword;
    }
}