namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("CreateNaturalPayerTestsHappyPath")]
public class CreateNaturalPayerTestsHappyPath : TestBaseSetup
{
    public CreateNaturalPayerTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC01")]
    [AllureLabel("TestCase", "TC01")]
    public async Task NaturalUserEndpoint_CreateUser_Successfully()
    {
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();
        
        var results = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.UserIdPrefix);
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC01")]
    [AllureLabel("TestCase", "TC02")]
    public async Task NaturalUserEndpoint_CreatedUserIsUnique_Successfully()
    {
        UserNaturalPayerPostDTO userNaturalPayerPostDto = UserFactory.CreateValidUser();
        
        var firstResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        var secondResults = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        
        firstResults.Id.ShouldNotBe(secondResults.Id);
    }
}