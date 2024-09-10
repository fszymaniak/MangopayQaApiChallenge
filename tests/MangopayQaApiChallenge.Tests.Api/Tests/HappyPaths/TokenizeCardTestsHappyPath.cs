namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

public class TokenizeCardTestsHappyPath : TestBaseSetup
{
    private CardRegistrationDTO _cardRegistrationResponse = null!;
    
    public TokenizeCardTestsHappyPath() : base(new MangoPayApi())
    {
    }

    [SetUp]
    public void SetUp()
    {
        var userNaturalRequestData = UserFactory.CreateValidUser();
        var userNaturalResponse = Api.Users.CreatePayerAsync(userNaturalRequestData).Result;
        var cardRegistrationDto = CardFactory.CreateValidCard(userNaturalResponse.Id);
        _cardRegistrationResponse = Api.CardRegistrations.CreateAsync(cardRegistrationDto).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task TokenizeCardEndpoint_TokenizeCard_Successfully()
    {
        
        CardRegistrationPostDTO cardRegistrationDto = CardFactory.CreateValidCard(_userNaturalResponse.Id);
        
        var results = await Api.CardRegistrations.CreateAsync(cardRegistrationDto);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.CardIdPrefix);
        results.Status.ShouldBe(CardStatus.CREATED.ToString());
    }
    
    
}