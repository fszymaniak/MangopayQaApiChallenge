namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.InvalidInputs;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("UnhappyPaths")]
[AllureSubSuite("CreateUserInvalidInputsTests")]
public class CreateUserInvalidInputsTests : TestBaseSetup
{
    public CreateUserInvalidInputsTests() : base(new MangoPayApi())
    {
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC02")]
    [AllureLabel("TestCase", "TC05")]
    public async Task UserEndpoint_TryToCreateUserWithInvalidEmail_BadRequest()
    {
        // Given
        UserDTO response = null!;
        var invalidEmail = "email_invalid@";
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
            exception.Message.ShouldContain($"The field Email must match the regular expression");
        }
        
        // Then
        response.ShouldBe(null);
        await StatusCodeValidator.ValidateStatusCode400BadRequest();
    }
}