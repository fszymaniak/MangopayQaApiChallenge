using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.InvalidInputs;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite(AllureMetadata.Suites.UnhappyPaths)]
[AllureSubSuite("CreateUserInvalidInputsTests")]
public class CreateUserInvalidInputsTests : TestBaseSetup
{
    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, AllureMetadata.AcceptanceCriteria.AC02)]
    [AllureLabel(AllureMetadata.Labels.TestCase, AllureMetadata.TestCase.TC05)]
    public async Task UserEndpoint_TryToCreateUserWithInvalidEmail_BadRequest()
    {
        // Given
        UserDTO response = null!;
        var invalidEmail = InvalidData.InvalidEmailFormat;
        var userRequestData = UserFactory.CreateValidUser();
        userRequestData.Email = invalidEmail;

        // When
        try
        {
            response = await Api.Users.CreatePayerAsync(userRequestData);
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(ResponseException));
            exception.Message.ShouldContain(ErrorMessages.EmailValidationError);
        }

        // Then
        response.ShouldBe(null);
        await StatusCodeValidator.ValidateStatusCode400BadRequestAsync();
    }
}