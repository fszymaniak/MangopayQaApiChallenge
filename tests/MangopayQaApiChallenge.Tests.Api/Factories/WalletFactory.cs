using MangoPay.SDK.Core.Enumerations;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class WalletFactory : IWalletFactory
{
    public WalletPostDTO CreateValidWallet(List<string> walletOwners)
    {
        return new WalletPostDTO
        (
            walletOwners,
            "E-Money wallet",
            CurrencyIso.EUR
        );


    }
}