using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite(AllureMetadata.Suites.HappyPaths)]
[AllureSubSuite("CreateNaturalPayerTestsHappyPath")]
public class CreateNaturalPayerTestsHappyPath : TestBaseSetup
{
    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC01)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC01)]
    public async Task NaturalUserEndpoint_CreateUser_Successfully()
    {
        // Given
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();

        // When
        var results = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);

        // Then
        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(results.Id, IdPrefixes.UserIdPrefix);
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC01)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC02)]
    public async Task NaturalUserEndpoint_CreatedUserIsUnique_Successfully()
    {
        // Given
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();
        
        // When
        var firstResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        var secondResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        
        // Then
        firstResults.Id.ShouldNotBe(secondResults.Id);
    }
}