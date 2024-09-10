using MangoPay.SDK;
using Microsoft.Extensions.Configuration;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class TestBaseSetup
{
    protected readonly MangoPayApi Api;
    protected readonly IUserFactory UserFactory;
    protected readonly IStatusCodeValidator StatusCodeValidator;
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
        StatusCodeValidator = new StatusCodeValidator(Api);

    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        Api.Config.ClientId = _appSettings.ClientId;
        Api.Config.ClientPassword = _appSettings.ClientPassword;
    }
}