namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("TokenizeCardTestsHappyPath")]
public class TokenizeCardTestsHappyPath : TestBaseSetup
{
    private CardRegistrationDTO _cardRegistrationResponse = null!;
    
    public TokenizeCardTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        var userNaturalRequestData = UserFactory.CreateValidUser();
        var userNaturalResponse = Api.Users.CreatePayerAsync(userNaturalRequestData).Result;
        var cardRegistrationDto = CardFactory.CreateValidCard(userNaturalResponse.Id);
        _cardRegistrationResponse = Api.CardRegistrations.CreateAsync(cardRegistrationDto).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC04")]
    [AllureLabel("TestCase", "TC01")]
    public async Task TokenizeCardEndpoint_TokenizeCard_Successfully()
    {
        var tokenizeRequest = CardFactory.CreateValidTokenizeRequest(_cardRegistrationResponse);
        var result =  await RestSharpDriver.SendPostRequestToTokenizeCard(tokenizeRequest);
        var registrationData = result.Content;
        
        registrationData.ShouldNotBe(null);
        registrationData.ShouldStartWith("data=");
    }
}