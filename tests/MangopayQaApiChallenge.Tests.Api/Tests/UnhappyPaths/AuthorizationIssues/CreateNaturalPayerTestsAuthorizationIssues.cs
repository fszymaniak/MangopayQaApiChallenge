using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.AuthorizationIssues;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite(AllureMetadata.Suites.UnhappyPaths)]
[AllureSubSuite("CreateNaturalPayerTestsAuthorizationIssues")]
public class CreateNaturalPayerTestsAuthorizationIssues : TestBaseSetup
{
    private UserNaturalPayerPostDTO _userNaturalPayerPostDto = null!;

    [SetUp]
    public void SetUp()
    {
        _userNaturalPayerPostDto =  UserFactory.CreateValidUser();
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC01)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC03)]
    public async Task NaturalUserEndpoint_TryToCreateUserWithInvalidClientId_Unauthorized()
    {
        // Given
        UserNaturalDTO response = null!;
        Api.Config.ClientId = InvalidData.InvalidClientId;

        // When
        response = await CallNaturalUserEndpointWithInvalidCredentialsAndValidateResponse(response, _userNaturalPayerPostDto);

        // Then
        response.ShouldBe(null);
        await StatusCodeValidator.ValidateStatusCode401UnauthorizedAsync();
    }

    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC01)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC04)]
    public async Task NaturalUserEndpoint_TryToCreateUserWithInvalidClientPassword_Unauthorized()
    {
        // Given
        UserNaturalDTO response = null!;
        Api.Config.ClientPassword = InvalidData.InvalidClientPassword;

        // When
        response = await CallNaturalUserEndpointWithInvalidCredentialsAndValidateResponse(response, _userNaturalPayerPostDto);

        // Then
        response.ShouldBe(null);
        await StatusCodeValidator.ValidateStatusCode401UnauthorizedAsync();
    }

    private async Task<UserNaturalDTO> CallNaturalUserEndpointWithInvalidCredentialsAndValidateResponse(UserNaturalDTO response, UserNaturalPayerPostDTO userNaturalPayerPostDto)
    {
        try
        {
            response = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(UnauthorizedAccessException));
            exception.Message.ShouldContain(ErrorMessages.InvalidClientError);
        }

        return response;
    }
}