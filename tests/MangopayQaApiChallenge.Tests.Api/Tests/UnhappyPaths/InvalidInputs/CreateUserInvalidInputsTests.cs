namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.InvalidInputs;

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
        UserDTO response = null!;
        var invalidEmail = "email_invalid@";
        var userRequestData = UserFactory.CreateValidUser();
        userRequestData.Email = invalidEmail;
        
        try
        {
            response = await Api.Users.CreatePayerAsync(userRequestData);
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(ResponseException));
            exception.Message.ShouldContain($"The field Email must match the regular expression");
        }
        
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode400BadRequest();
    }
}