namespace MangopayQaApiChallenge.Tests.Api.Tests.HappyPaths;

[AllureFeature("Manage financial transactions")]
[AllureLabel("UserStory", "#01")]
[AllureSuite("HappyPaths")]
[AllureSubSuite("PayInTestsHappyPath")]
public class PayInTestsHappyPath : TestBaseSetup
{
    private UserNaturalDTO _userNaturalResponse = null!;
    private WalletDTO _walletResponse = null!;
    private CardRegistrationDTO _cardRegistrationResponse = null!;
    private readonly UserPayerSteps _userPayerSteps = new UserPayerSteps();
    private readonly WalletSteps _walletSteps = new WalletSteps();
    private readonly CardSteps _cardSteps = new CardSteps();
    
    public PayInTestsHappyPath() : base(new MangoPayApi())
    {
    }
    
    [SetUp]
    public void SetUp()
    {
        _userNaturalResponse = _userPayerSteps.CreateUserViaPostApiCall(UserFactory, Api).Result;
        _walletResponse = _walletSteps.CreateWalletViaPostApiCall(new List<string>() { _userNaturalResponse.Id }, WalletFactory, Api).Result;
        var cardRegistrationResponse = _cardSteps.RegisterCardViaPostApiCall(_userNaturalResponse.Id, CardFactory, Api).Result;
        var tokenizeResponse = _cardSteps.TokenizeCardViaPostApiCall(cardRegistrationResponse, CardFactory, RestSharpDriver).Result;
        var registrationData = tokenizeResponse!.Content;
        _cardRegistrationResponse = _cardSteps.UpdateRegisteredCardViaPutApiCall(registrationData!, cardRegistrationResponse.Id, CardFactory, Api).Result;
    }
    
    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task PayInEndpoint_MadePayIn_Successfully()
    {
        int debitedAmount = 10000;
        var directPayInRequest = PayInFactory.CreateValidDirectPayIn(_userNaturalResponse, _walletResponse,
            _cardRegistrationResponse, amount: debitedAmount, CurrencyIso.EUR);
        var results = await Api.PayIns.CreateCardDirectAsync(directPayInRequest);
        await StatusCodeValidator.ValidateStatusCode200Ok();
        IdValidator.ValidateId(results.Id, IdPrefixes.PayInIdPrefix);
        results.Status.ShouldBe(TransactionStatus.CREATED);
        results.PaymentType.ShouldBe(PayInPaymentType.CARD);
        results.DebitedFunds.Amount.ShouldBe(debitedAmount);

        var creditedFounds = debitedAmount - results.Fees.Amount;
        results.CreditedFunds.Amount.ShouldBe(creditedFounds);
    }
}