namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class FactoriesSetup
{
    protected readonly IUserFactory UserFactory;
    protected readonly IWalletFactory WalletFactory;
    protected readonly ICardFactory CardFactory;
    protected readonly IPayInFactory PayInFactory;
    private readonly IPathProvider _pathProvider = new PathProvider();
    private readonly IUserValuesRandomizer _userValuesRandomizer = new UserValuesRandomizer();

    public FactoriesSetup()
    {
        UserFactory = new UserFactory(_pathProvider, _userValuesRandomizer);
        WalletFactory = new WalletFactory();
        CardFactory = new CardFactory();
        PayInFactory = new PayInFactory(_pathProvider, _userValuesRandomizer);
    }
}