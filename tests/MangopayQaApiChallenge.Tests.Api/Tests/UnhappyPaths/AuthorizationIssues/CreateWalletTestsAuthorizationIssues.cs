namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.AuthorizationIssues;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("UnhappyPaths")]
[AllureSubSuite("CreateWalletTestsAuthorizationIssues")]
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
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC03")]
    public async Task WalletEndpoint_TryToCreateWalletWithInvalidClientId_Unauthorized()
    {
        // Given
        WalletDTO response = null!;
        Api.Config.ClientId = InvalidData.InvalidClientId;
        
        // When
        response = await CallWalletEndpointWithInvalidCredentialsAndValidateResponse(response, _walletRequestData);
        
        // Then
        response.ShouldBe(null);
        await StatusCodeValidator.ValidateStatusCode401Unauthorized();
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC04")]
    public async Task WalletEndpoint_TryToCreateWalletWithInvalidClientPassword_Unauthorized()
    {
        // Given
        WalletDTO response = null!;
        Api.Config.ClientId = InvalidData.InvalidClientPassword;
        
        // When
        response = await CallWalletEndpointWithInvalidCredentialsAndValidateResponse(response, _walletRequestData);
        
        // Then
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