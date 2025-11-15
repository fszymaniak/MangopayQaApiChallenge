using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite(AllureMetadata.Suites.HappyPaths)]
[AllureSubSuite("TokenizeCardTestsHappyPath")]
public class TokenizeCardTestsHappyPath : TestBaseSetup
{
    private CardRegistrationDTO _cardRegistrationResponse = null!;

    [SetUp]
    public async Task SetUp()
    {
        var userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCallAsync(UserFactory, Api);
        _cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCallAsync(userNaturalResponse.Id, CardFactory, Api);
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC04)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC01)]
    public async Task TokenizeCardEndpoint_TokenizeCard_Successfully()
    {
        // Given and when
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCallAsync(_cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;
        
        // Then
        registrationData.ShouldNotBe(null);
        registrationData.ShouldStartWith(TestDataConstants.RegistrationDataPrefix);
    }
}