﻿namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("CreateWalletTestsHappyPath")]
public class CreateWalletTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;
    private readonly UserPayerSteps _userPayerSteps = new UserPayerSteps();
    private readonly WalletSteps _walletSteps = new WalletSteps();
    
    public CreateWalletTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        _userNaturalResponse = _userPayerSteps.CreateUserViaPostApiCall(UserFactory, Api).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC01")]
    public async Task WalletEndpoint_CreateWallet_Successfully()
    {
        List<string> userIdsList = new List<string> { _userNaturalResponse.Id };

        var results = await _walletSteps.CreateWalletViaPostApiCall(userIdsList, WalletFactory, Api);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.WalletIdPrefix);
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC01")]
    public async Task WalletEndpoint_CreatedWalletIsUnique_Successfully()
    {
        var userId = _userNaturalResponse.Id;
        List<string> userIdsList = new List<string> { userId };
        WalletPostDTO walletPostDto = WalletFactory.CreateValidWallet(userIdsList);
        
        var firstResults = await Api.Wallets.CreateAsync(walletPostDto);
        var secondResults = await Api.Wallets.CreateAsync(walletPostDto);
        
        firstResults.Id.ShouldNotBe(secondResults.Id);
    }
}