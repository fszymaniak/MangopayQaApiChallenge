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

    [SetUp]
    public async Task SetUp()
    {
        _userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCallAsync(UserFactory, Api);
        _walletResponse = await WalletSteps.CreateWalletViaPostApiCallAsync(new List<string>() { _userNaturalResponse.Id }, WalletFactory, Api);
        var cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCallAsync(_userNaturalResponse.Id, CardFactory, Api);
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCallAsync(cardRegistrationResponse, CardFactory, RestSharpDriver);
        var registrationData = tokenizeResponse!.Content;
        _cardRegistrationResponse = await CardSteps.UpdateRegisteredCardViaPutApiCallAsync(registrationData!, cardRegistrationResponse.Id, CardFactory, Api);
    }

    [Test]
    [AllureLabel("AcceptanceCriteria", "AC03")]
    [AllureLabel("TestCase", "TC01")]
    public async Task PayInEndpoint_MadePayIn_Successfully()
    {
        // Given
        int debitedAmount = TestDataConstants.DefaultDebitedAmount;
        var directPayInRequest = PayInFactory.CreateValidDirectPayIn(_userNaturalResponse, _walletResponse,
            _cardRegistrationResponse, amount: debitedAmount, CurrencyIso.EUR);

        // When
        var results = await Api.PayIns.CreateCardDirectAsync(directPayInRequest);

        // Then
        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(results.Id, IdPrefixes.PayInIdPrefix);
        results.Status.ShouldBe(TransactionStatus.CREATED);
        results.PaymentType.ShouldBe(PayInPaymentType.CARD);
        results.DebitedFunds.Amount.ShouldBe(debitedAmount);

        var creditedFounds = debitedAmount - results.Fees.Amount;
        results.CreditedFunds.Amount.ShouldBe(creditedFounds);
    }
}