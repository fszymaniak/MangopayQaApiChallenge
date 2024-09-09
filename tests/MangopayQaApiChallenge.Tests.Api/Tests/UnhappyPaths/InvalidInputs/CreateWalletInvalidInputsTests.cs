﻿namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.InvalidInputs;

public class CreateWalletInvalidInputsTests : TestBaseSetup
{
    public CreateWalletInvalidInputsTests() : base(new MangoPayApi())
    {
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC05")]
    public async Task WalletEndpoint_TryToCreateWalletWithNotExistingUserId_BadRequest()
    {
        WalletDTO response = null!;
        var notExistingUserId = "notExistingUserId";
        List<string> userIdsList = new List<string> { notExistingUserId };
        var walletRequestData = WalletFactory.CreateValidWallet(userIdsList);
        
        try
        {
            response = await Api.Wallets.CreateAsync(walletRequestData);
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(ResponseException));
            exception.Message.ShouldContain($"The value {notExistingUserId} is not valid");
        }
        
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode400BadRequest();
    }
}