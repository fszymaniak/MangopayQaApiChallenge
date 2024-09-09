namespace MangopayQaApiChallenge.Tests.Api.Factories;

public interface IWalletFactory
{
    public WalletPostDTO CreateValidWallet(List<string> walletOwners);
}