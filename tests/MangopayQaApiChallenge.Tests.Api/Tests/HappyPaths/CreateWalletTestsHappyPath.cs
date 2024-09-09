﻿namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

public class CreateWalletTestsHappyPath : TestBaseSetup
{
    private UserNaturalPayerPostDTO _userNaturalRequestData = null!;
    private UserNaturalDTO _userNaturalResponse = null!;
    
    public CreateWalletTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        _userNaturalRequestData = UserFactory.CreateValidUser();
        _userNaturalResponse = Api.Users.CreatePayerAsync(_userNaturalRequestData).Result;

    }
    
    [Test]
    public async Task WalletEndpoint_CreateWallet_Successfully()
    {
        List<string> userIdsList = new List<string> { _userNaturalResponse.Id };
        WalletPostDTO walletPostDto = WalletFactory.CreateValidWallet(userIdsList);
        
        var results = await Api.Wallets.CreateAsync(walletPostDto);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.WalletIdPrefix);
    }
    
    [Test]
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