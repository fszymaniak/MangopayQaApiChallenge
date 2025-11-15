using Autofac;
using MangopayQaApiChallenge.Tests.Api.Logging;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

[AllureNUnit]
public class TestBaseSetup : FactoriesSetup
{
    protected readonly MangoPayApi Api;
    protected readonly IStatusCodeValidator StatusCodeValidator;
    protected readonly IIdValidator IdValidator;
    protected readonly IRestSharpDriver RestSharpDriver;
    protected readonly UserPayerSteps UserPayerSteps;
    protected readonly WalletSteps WalletSteps;
    protected readonly CardSteps CardSteps;
    private readonly AppSettings _appSettings;
    protected IContainer Container { get; private set; } = null!;

    public TestBaseSetup() : this(new MangoPayApi())
    {
    }

    public TestBaseSetup(MangoPayApi api)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.AppSettingsFileName, false)
            .AddUserSecrets<TestBaseSetup>()
            .Build();

        _appSettings = config.Get<AppSettings>() ?? throw new InvalidOperationException(ConfigurationConstants.AppSettingsMissingError);

        if (string.IsNullOrWhiteSpace(_appSettings.ClientId))
            throw new InvalidOperationException(ConfigurationConstants.ClientIdRequiredError);

        if (string.IsNullOrWhiteSpace(_appSettings.ClientPassword))
            throw new InvalidOperationException(ConfigurationConstants.ClientPasswordRequiredError);

        Api = api;

        // Build DI container
        Container = DependencyInjectionConfig.BuildContainer(Api);

        // Resolve dependencies from container
        StatusCodeValidator = Container.Resolve<IStatusCodeValidator>();
        IdValidator = Container.Resolve<IIdValidator>();
        RestSharpDriver = Container.Resolve<IRestSharpDriver>();

        // Resolve factories from container
        UserFactory = Container.Resolve<IUserFactory>();
        WalletFactory = Container.Resolve<IWalletFactory>();
        CardFactory = Container.Resolve<ICardFactory>();
        PayInFactory = Container.Resolve<IPayInFactory>();

        // Resolve steps from container
        UserPayerSteps = Container.Resolve<UserPayerSteps>();
        WalletSteps = Container.Resolve<WalletSteps>();
        CardSteps = Container.Resolve<CardSteps>();
    }

    [OneTimeSetUp]
    public void Setup()
    {
        TestLogger.TestSetup("Configuring API credentials");
        Api.Config.ClientId = _appSettings.ClientId;
        Api.Config.ClientPassword = _appSettings.ClientPassword;
        TestLogger.TestSetup("API configuration completed");
    }

    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var outcome = TestContext.CurrentContext.Result.Outcome;

        if (outcome != ResultState.Success)
        {
            TestLogger.TestTearDown($"Test '{testName}' failed with outcome: {outcome}");
            // here you can add your screen shot
            // AllureApi.AddScreenDiff("expected.png", "actual.png", "diff.png");
        }
        else
        {
            TestLogger.TestTearDown($"Test '{testName}' completed successfully");
        }
    }
}