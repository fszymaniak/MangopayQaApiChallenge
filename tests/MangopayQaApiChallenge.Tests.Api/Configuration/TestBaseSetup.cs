namespace MangopayQaApiChallenge.Tests.Api.Configuration;

[AllureNUnit]
public class TestBaseSetup : FactoriesSetup
{
    protected readonly MangoPayApi Api;
    protected readonly IStatusCodeValidator StatusCodeValidator;
    protected readonly IIdValidator IdValidator;
    protected readonly IRestSharpDriver RestSharpDriver;
    private readonly AppSettings _appSettings;

    public TestBaseSetup(MangoPayApi api)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", false)
            .AddUserSecrets<TestBaseSetup>()
            .Build();

        _appSettings = config.Get<AppSettings>()!;
        Api = api;

        StatusCodeValidator = new StatusCodeValidator(Api);
        IdValidator = new IdValidator();
        RestSharpDriver = new RestSharpDriver();
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