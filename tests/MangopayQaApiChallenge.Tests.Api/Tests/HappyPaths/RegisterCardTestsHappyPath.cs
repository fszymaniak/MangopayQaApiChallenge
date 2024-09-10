namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("RegisterCardTestsHappyPath")]
public class RegisterCardTestsHappyPath : TestBaseSetup
{
    private UserNaturalPayerPostDTO _userNaturalRequestData = null!;
    private UserNaturalDTO _userNaturalResponse = null!;
    
    public RegisterCardTestsHappyPath() : base(new MangoPayApi())
    {
    }
    
    [SetUp]
    public void SetUp()
    {
        _userNaturalRequestData = UserFactory.CreateValidUser();
        _userNaturalResponse = Api.Users.CreatePayerAsync(_userNaturalRequestData).Result;

    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_RegisterCard_Successfully()
    {
        CardRegistrationPostDTO cardRegistrationDto = CardFactory.CreateValidCard(_userNaturalResponse.Id);
        
        var results = await Api.CardRegistrations.CreateAsync(cardRegistrationDto);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.CardIdPrefix);
        results.Status.ShouldBe(CardStatus.CREATED.ToString());
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC05")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_UpdateCard_Successfully()
    {
        var cardRegistrationDto = CardFactory.CreateValidCard(_userNaturalResponse.Id);
        var cardRegistrationResponse = Api.CardRegistrations.CreateAsync(cardRegistrationDto).Result;
        var tokenizeRequest = CardFactory.CreateValidTokenizeRequest(cardRegistrationResponse);
        var tokenizeResponse =  await RestSharpDriver.SendPostRequestToTokenizeCard(tokenizeRequest);
        var registrationData = tokenizeResponse.Content;
        var cardUpdateRequest = CardFactory.CreateValidCardUpdateRequest(registrationData!);
        
        var results = await Api.CardRegistrations.UpdateAsync(cardUpdateRequest, cardRegistrationResponse.Id);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        results.RegistrationData.ShouldBe(registrationData);
        results.Status.ShouldBe(CardStatus.VALIDATED.ToString());
    }
}