namespace MangopayQaApiChallenge.Tests.Api.Steps;

public class WalletSteps
{
    public WalletSteps()
    {
    }
    
    public async Task<WalletDTO> CreateWalletViaPostApiCall(List<string> walletOwnersId, IWalletFactory userFactory, MangoPayApi api)
    {
        var walletRequestData = userFactory.CreateValidWallet(walletOwnersId);
        var walletResponse = await api.Wallets.CreateAsync(walletRequestData);
        
        return walletResponse;
    }
}