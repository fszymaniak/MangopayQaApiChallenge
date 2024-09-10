namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("RegisterCardTestsHappyPath")]
public class RegisterCardTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;
    private readonly UserPayerSteps _userPayerSteps = new UserPayerSteps();
    private readonly CardSteps _cardSteps = new CardSteps();
    
    public RegisterCardTestsHappyPath() : base(new MangoPayApi())
    {
    }
    
    [SetUp]
    public void SetUp()
    {
        _userNaturalResponse = _userPayerSteps.CreateUserViaPostApiCall(UserFactory, Api).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_RegisterCard_Successfully()
    {
        var results = await _cardSteps.RegisterCardViaPostApiCall(_userNaturalResponse.Id, CardFactory, Api);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.CardIdPrefix);
        results.Status.ShouldBe(CardStatus.CREATED.ToString());
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC05")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_UpdateCard_Successfully()
    {
        var cardRegistrationResponse = await _cardSteps.RegisterCardViaPostApiCall(_userNaturalResponse.Id, CardFactory, Api);
        var tokenizeResponse = await _cardSteps.TokenizeCardViaPostApiCall(cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;
        var results = await _cardSteps.UpdateRegisteredCardViaPutApiCall(registrationData!, cardRegistrationResponse.Id, CardFactory, Api);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        results.RegistrationData.ShouldBe(registrationData);
        results.Status.ShouldBe(CardStatus.VALIDATED.ToString());
    }
}