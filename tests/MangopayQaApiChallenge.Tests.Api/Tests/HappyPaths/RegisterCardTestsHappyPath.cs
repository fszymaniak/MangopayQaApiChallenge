namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("RegisterCardTestsHappyPath")]
public class RegisterCardTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;

    public RegisterCardTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public async Task SetUp()
    {
        _userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCall(UserFactory, Api);
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_RegisterCard_Successfully()
    {
        // Given and When
        var results = await CardSteps.RegisterCardViaPostApiCall(_userNaturalResponse.Id, CardFactory, Api);
        
        // Then
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.CardIdPrefix);
        results.Status.ShouldBe(CardStatus.CREATED.ToString());
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC05")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_UpdateCard_Successfully()
    {
        // Given
        var cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCall(_userNaturalResponse.Id, CardFactory, Api);
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCall(cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;

        // When
        var results = await CardSteps.UpdateRegisteredCardViaPutApiCall(registrationData!, cardRegistrationResponse.Id, CardFactory, Api);
        
        // Then
        await StatusCodeValidator.ValidateStatusCode200Ok();
        results.RegistrationData.ShouldBe(registrationData);
        results.Status.ShouldBe(CardStatus.VALIDATED.ToString());
    }
}