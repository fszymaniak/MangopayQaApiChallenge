using MangoPay.SDK.Core.Enumerations;

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
    
    
    public PayInTestsHappyPath() : base(new MangoPayApi())
    {
    }
    
    [SetUp]
    public void SetUp()
    {
        var userNaturalRequestData = UserFactory.CreateValidUser();
        _userNaturalResponse = Api.Users.CreatePayerAsync(userNaturalRequestData).Result;
        WalletPostDTO walletRequestData = WalletFactory.CreateValidWallet(new List<string>() { _userNaturalResponse.Id });
        _walletResponse = Api.Wallets.CreateAsync(walletRequestData).Result;
        var cardRegistrationRequest = CardFactory.CreateValidCard(_userNaturalResponse.Id);
        _cardRegistrationResponse = Api.CardRegistrations.CreateAsync(cardRegistrationRequest).Result;
        var tokenizeRequest = CardFactory.CreateValidTokenizeRequest(_cardRegistrationResponse);
        var tokenizeResponse = RestSharpDriver.SendPostRequestToTokenizeCard(tokenizeRequest).Result;
        var registrationData = tokenizeResponse.Content;
        var cardUpdateRequest = CardFactory.CreateValidCardUpdateRequest(registrationData!);
        _cardRegistrationResponse = Api.CardRegistrations.UpdateAsync(cardUpdateRequest, _cardRegistrationResponse.Id).Result;
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