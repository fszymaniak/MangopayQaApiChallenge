namespace MangopayQaApiChallenge.Tests.Api.Tests.UnhappyPaths.AuthorizationIssues;

[TestFixture]
[AllureNUnit]
public class CreateNaturalPayerTestsAuthorizationIssues : TestBaseSetup
{
    private UserNaturalPayerPostDTO _userNaturalPayerPostDto = null!;
    
    public CreateNaturalPayerTestsAuthorizationIssues() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        _userNaturalPayerPostDto =  UserFactory.CreateValidUser();
    }
    
    [Test]
    public async Task NaturalUserEndpoint_TryToCreateUserWithInvalidClientId_Unauthorized()
    {
        UserNaturalDTO response = null!;

        Api.Config.ClientId = InvalidData.InvalidClientId;
        response = await CallNaturalUserEndpointWithInvalidCredentialsAndValidateResponse(response, _userNaturalPayerPostDto);
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode401Unauthorized();
    }
    
    [Test]
    public async Task NaturalUserEndpoint_TryToCreateUserWithInvalidClientPassword_Unauthorized()
    {
        UserNaturalDTO response = null!;

        Api.Config.ClientPassword = InvalidData.InvalidClientPassword;
        response = await CallNaturalUserEndpointWithInvalidCredentialsAndValidateResponse(response, _userNaturalPayerPostDto);
        response.ShouldBe(null);
        
        await StatusCodeValidator.ValidateStatusCode401Unauthorized();
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
            exception.Message.ShouldContain("invalid_client");
        }

        return response;
    }
}