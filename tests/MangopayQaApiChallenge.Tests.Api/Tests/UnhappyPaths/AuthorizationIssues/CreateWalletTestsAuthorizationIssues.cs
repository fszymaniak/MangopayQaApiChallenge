namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.AuthorizationIssues;

[TestFixture]
[AllureNUnit]
public class CreateWalletTestsAuthorizationIssues : TestBaseSetup
{
    private WalletPostDTO _walletRequestData = null!;
    
    public CreateWalletTestsAuthorizationIssues() : base(new MangoPayApi())
    {
    }
    
    [SetUp]
    public void SetUp()
    {
        var userNaturalRequestData =  UserFactory.CreateValidUser();
        var userNaturalResponse = Api.Users.CreatePayerAsync(userNaturalRequestData).Result;
        List<string> userIdsList = new List<string> { userNaturalResponse.Id };
        _walletRequestData = WalletFactory.CreateValidWallet(userIdsList);
    }
    
    [Test]
    public async Task WalletEndpoint_TryToCreateWalletWithInvalidClientId_Unauthorized()
    {
        WalletDTO response = null!;

        Api.Config.ClientId = InvalidData.InvalidClientId;
        response = await CallWalletEndpointWithInvalidCredentialsAndValidateResponse(response, _walletRequestData);
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode401Unauthorized();
    }
    
    [Test]
    public async Task WalletEndpoint_TryToCreateWalletWithInvalidClientPassword_Unauthorized()
    {
        WalletDTO response = null!;

        Api.Config.ClientId = InvalidData.InvalidClientPassword;
        response = await CallWalletEndpointWithInvalidCredentialsAndValidateResponse(response, _walletRequestData);
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode401Unauthorized();
    }
    
    private async Task<WalletDTO> CallWalletEndpointWithInvalidCredentialsAndValidateResponse(WalletDTO response, WalletPostDTO walletPostDto)
    {
        try
        {
            response = await Api.Wallets.CreateAsync(walletPostDto);
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(UnauthorizedAccessException));
            exception.Message.ShouldContain("invalid_client");
        }

        return response;
    }
}