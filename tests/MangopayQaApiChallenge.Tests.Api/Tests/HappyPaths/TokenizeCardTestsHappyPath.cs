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
    public async Task SetUp()
    {
        var userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCall(UserFactory, Api);
        _cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCall(userNaturalResponse.Id, CardFactory, Api);
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC04")]
    [AllureLabel("TestCase", "TC01")]
    public async Task TokenizeCardEndpoint_TokenizeCard_Successfully()
    {
        // Given and when
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCall(_cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;
        
        // Then
        registrationData.ShouldNotBe(null);
        registrationData.ShouldStartWith("data=");
    }
}