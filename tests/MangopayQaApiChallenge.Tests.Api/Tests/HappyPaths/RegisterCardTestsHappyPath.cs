namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("RegisterCardTestsHappyPath")]
public class RegisterCardTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;

    [SetUp]
    public async Task SetUp()
    {
        _userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCallAsync(UserFactory, Api);
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_RegisterCard_Successfully()
    {
        // Given and When
        var results = await CardSteps.RegisterCardViaPostApiCallAsync(_userNaturalResponse.Id, CardFactory, Api);

        // Then
        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(results.Id, IdPrefixes.CardIdPrefix);
        results.Status.ShouldBe(CardStatus.CREATED.ToString());
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC05")]
    [AllureLabel("TestCase", "TC01")]
    public async Task CardRegistrationEndpoint_UpdateCard_Successfully()
    {
        // Given
        var cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCallAsync(_userNaturalResponse.Id, CardFactory, Api);
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCallAsync(cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;

        // When
        var results = await CardSteps.UpdateRegisteredCardViaPutApiCallAsync(registrationData!, cardRegistrationResponse.Id, CardFactory, Api);

        // Then
        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        results.RegistrationData.ShouldBe(registrationData);
        results.Status.ShouldBe(CardStatus.VALIDATED.ToString());
    }
}