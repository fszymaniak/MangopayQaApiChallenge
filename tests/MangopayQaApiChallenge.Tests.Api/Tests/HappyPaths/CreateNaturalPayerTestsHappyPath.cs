namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

public class CreateNaturalPayerTestsHappyPath : TestBaseSetup
{
    public CreateNaturalPayerTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [Test]
    public async Task NaturalUserEndpoint_CreateUser_Successfully()
    {
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();
        
        var results = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.UserIdPrefix);
    }
    
    [Test]
    public async Task NaturalUserEndpoint_CreatedUserIsUnique_Successfully()
    {
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();
        
        var firstResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        var secondResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        
        firstResults.Id.ShouldNotBe(secondResults.Id);
    }
}