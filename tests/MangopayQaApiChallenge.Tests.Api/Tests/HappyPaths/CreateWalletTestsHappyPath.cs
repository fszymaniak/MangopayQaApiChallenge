namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("CreateWalletTestsHappyPath")]
public class CreateWalletTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;

    public CreateWalletTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public async Task SetUp()
    {
        _userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCall(UserFactory, Api);
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC01")]
    public async Task WalletEndpoint_CreateWallet_Successfully()
    {
        // Given
        List<string> userIdsList = new List<string> { _userNaturalResponse.Id };

        // When
        var results = await WalletSteps.CreateWalletViaPostApiCall(userIdsList, WalletFactory, Api);
        
        // Then
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.WalletIdPrefix);
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC01")]
    public async Task WalletEndpoint_CreatedWalletIsUnique_Successfully()
    {
        // Given
        var userId = _userNaturalResponse.Id;
        List<string> userIdsList = new List<string> { userId };
        WalletPostDTO walletPostDto = WalletFactory.CreateValidWallet(userIdsList);
        
        // When
        var firstResults = await Api.Wallets.CreateAsync(walletPostDto);
        var secondResults = await Api.Wallets.CreateAsync(walletPostDto);

        // Then
        firstResults.Id.ShouldNotBe(secondResults.Id);
    }
}