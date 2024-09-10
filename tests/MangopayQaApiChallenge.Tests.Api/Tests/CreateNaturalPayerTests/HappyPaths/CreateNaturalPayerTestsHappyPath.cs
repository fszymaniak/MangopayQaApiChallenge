using MangoPay.SDK;
using MangoPay.SDK.Entities.POST;
using MangopayQaApiChallenge.Tests.Api.Configuration;
using MangopayQaApiChallenge.Tests.Api.Factories;
using MangopayQaApiChallenge.Tests.Api.Tools.Providers;
using MangopayQaApiChallenge.Tests.Api.Tools.Randomizers;
using MangopayQaApiChallenge.Tests.Api.Validators;
using Shouldly;

namespace MangopayQaApiChallenge.Tests.Api.Tests.CreateNaturalPayerTests.HappyPaths;

public class CreateNaturalPayerTestsHappyPath : TestBaseSetup
{
    private readonly IUserFactory _userFactory;
    private readonly IPathProvider _pathProvider = new PathProvider();
    private readonly IUserValuesRandomizer _userValuesRandomizer = new UserValuesRandomizer();
    private readonly IStatusCodeValidator _statusCodeValidator;
    
    public CreateNaturalPayerTestsHappyPath() : base(new MangoPayApi())
    {
        _userFactory = new UserFactory(_pathProvider, _userValuesRandomizer);
        _statusCodeValidator = new StatusCodeValidator(Api);
    }

    [Test]
    public async Task NaturalUserEndpoint_CreateUser_Successfully()
    {
        UserNaturalPayerPostDTO userNaturalPayerPostDto = _userFactory.CreateValidUser();
        
        var clientId = Api.Config.ClientId;
        var results = await Api.Users.CreatePayerAsync(userNaturalPayerPostDto);
        await _statusCodeValidator.ValidateStatusCode200Ok();

        var userId = results.Id;
        userId.ShouldNotBe(null);
        userId.ShouldContain("user_m_");
    }
}