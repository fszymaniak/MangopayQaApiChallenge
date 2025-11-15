using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite(AllureMetadata.Suites.HappyPaths)]
[AllureSubSuite("CreateWalletTestsHappyPath")]
public class CreateWalletTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;

    [SetUp]
    public async Task SetUp()
    {
        _userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCallAsync(UserFactory, Api);
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC02)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC01)]
    public async Task WalletEndpoint_CreateWallet_Successfully()
    {
        // Given
        List<string> userIdsList = new List<string> { _userNaturalResponse.Id };

        // When
        var results = await WalletSteps.CreateWalletViaPostApiCallAsync(userIdsList, WalletFactory, Api);

        // Then
        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(results.Id, IdPrefixes.WalletIdPrefix);
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC02)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC01)]
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