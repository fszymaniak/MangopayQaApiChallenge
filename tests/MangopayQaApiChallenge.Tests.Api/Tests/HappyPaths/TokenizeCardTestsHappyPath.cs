namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("TokenizeCardTestsHappyPath")]
public class TokenizeCardTestsHappyPath : TestBaseSetup
{
    private CardRegistrationDTO _cardRegistrationResponse = null!;
    private UserPayerSteps _userSteps = new UserPayerSteps();
    private CardSteps _cardSteps = new CardSteps();
    
    public TokenizeCardTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        var userNaturalResponse = _userSteps.CreateUserViaPostApiCall(UserFactory, Api).Result;
        _cardRegistrationResponse = _cardSteps.RegisterCardViaPostApiCall(userNaturalResponse.Id, CardFactory, Api).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC04")]
    [AllureLabel("TestCase", "TC01")]
    public async Task TokenizeCardEndpoint_TokenizeCard_Successfully()
    {
        // Given and when
        var tokenizeResponse = await _cardSteps.TokenizeCardViaPostApiCall(_cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;
        
        // Then
        registrationData.ShouldNotBe(null);
        registrationData.ShouldStartWith("data=");
    }
}