namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class FactoriesSetup
{
    protected IUserFactory UserFactory { get; set; } = null!;
    protected IWalletFactory WalletFactory { get; set; } = null!;
    protected ICardFactory CardFactory { get; set; } = null!;
    protected IPayInFactory PayInFactory { get; set; } = null!;
}