namespace MangopayQaApiChallenge.Tests.Api.Steps;

public class WalletSteps
{
    public async Task<WalletDTO> CreateWalletViaPostApiCallAsync(List<string> walletOwnersId, IWalletFactory walletFactory, MangoPayApi api)
    {
        var walletRequestData = walletFactory.CreateValidWallet(walletOwnersId);
        var walletResponse = await api.Wallets.CreateAsync(walletRequestData);

        return walletResponse;
    }
}