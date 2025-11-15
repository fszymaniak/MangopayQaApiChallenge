using MangopayQaApiChallenge.Tests.Api.Constants;

namespace MangopayQaApiChallenge.Tests.Api.Tests.E2E;

[AllureFeature(AllureMetadata.DefaultFeature)]
[AllureLabel(AllureMetadata.Labels.UserStory, AllureMetadata.DefaultUserStory)]
[AllureSuite("E2E")]
[AllureSubSuite("Complete User Story Flow")]
public class UserStoryEndToEndTests : TestBaseSetup
{
    [Test]
    [AllureLabel(AllureMetadata.Labels.AcceptanceCriteria, "AC01,AC02,AC03,AC04,AC05,AC06")]
    [AllureLabel(AllureMetadata.Labels.TestCase, "E2E_TC01")]
    [Description("End-to-end test covering the complete user story flow from user creation to PayIn")]
    public async Task CompleteUserStoryFlow_CreateUserWalletCardAndPayIn_Successfully()
    {
        // AC1: Create a new user
        // Given a client has valid client credentials
        // When the client requests to create a new user via the API
        // Then the API should return a success status and a unique user ID
        var userNaturalResponse = await UserPayerSteps.CreateUserViaPostApiCallAsync(UserFactory, Api);

        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(userNaturalResponse.Id, IdPrefixes.UserIdPrefix);
        userNaturalResponse.Id.ShouldNotBeNullOrEmpty();

        // AC2: Create a wallet for the user
        // Given a client has valid client credentials and the new user is created
        // When the client requests to create a new wallet for an existing user via the API
        // Then the API should return a success status and a unique wallet ID
        var walletResponse = await WalletSteps.CreateWalletViaPostApiCallAsync(
            new List<string> { userNaturalResponse.Id },
            WalletFactory,
            Api);

        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(walletResponse.Id, IdPrefixes.WalletIdPrefix);
        walletResponse.Id.ShouldNotBeNullOrEmpty();
        walletResponse.Owners.ShouldContain(userNaturalResponse.Id);

        // AC3: Create a card registration
        // Given a client has valid client credentials and the new user is created
        // When the client requests to create a new card registration for an existing user via the API
        // Then the API should return a success status and a unique card ID
        // And the card Status should be "CREATED"
        var cardRegistrationResponse = await CardSteps.RegisterCardViaPostApiCallAsync(
            userNaturalResponse.Id,
            CardFactory,
            Api);

        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(cardRegistrationResponse.Id, IdPrefixes.CardRegistrationIdPrefix);
        cardRegistrationResponse.Id.ShouldNotBeNullOrEmpty();
        cardRegistrationResponse.Status.ShouldBe(CardStatus.Created);

        // AC4: Tokenize the card
        // Given a client has valid client credentials
        // When the client requests to tokenize a newly created card via the API
        // Then the API should return a success status and a Registration Data
        var tokenizeResponse = await CardSteps.TokenizeCardViaPostApiCallAsync(
            cardRegistrationResponse,
            CardFactory,
            RestSharpDriver);

        tokenizeResponse.ShouldNotBeNull();
        tokenizeResponse.Content.ShouldNotBeNullOrEmpty();
        var registrationData = tokenizeResponse.Content;

        // AC5: Update card registration with validation
        // Given a client has valid client credentials and Registration Data Key
        // When the client requests to update a card registration with the registration data via the API
        // Then the API should return a success status and updated card data including RegistrationData and CardId
        // And the card Status should be "VALIDATED"
        var updatedCardRegistrationResponse = await CardSteps.UpdateRegisteredCardViaPutApiCallAsync(
            registrationData,
            cardRegistrationResponse.Id,
            CardFactory,
            Api);

        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        updatedCardRegistrationResponse.RegistrationData.ShouldNotBeNullOrEmpty();
        updatedCardRegistrationResponse.CardId.ShouldNotBeNullOrEmpty();
        updatedCardRegistrationResponse.Status.ShouldBe(CardStatus.Validated);
        IdValidator.ValidateId(updatedCardRegistrationResponse.CardId, IdPrefixes.CardIdPrefix);

        // AC6: Create a direct card PayIn
        // Given a client has valid client credentials and a new user, wallet, validated card
        // When the client requests to create a direct card pay-in via the API
        // Then the API should return a success status and pay in data with id
        int debitedAmount = TestDataConstants.DefaultDebitedAmount;
        var directPayInRequest = PayInFactory.CreateValidDirectPayIn(
            userNaturalResponse,
            walletResponse,
            updatedCardRegistrationResponse,
            amount: debitedAmount,
            CurrencyIso.EUR);

        var payInResponse = await Api.PayIns.CreateCardDirectAsync(directPayInRequest);

        await StatusCodeValidator.ValidateStatusCode200OkAsync();
        IdValidator.ValidateId(payInResponse.Id, IdPrefixes.PayInIdPrefix);
        payInResponse.Id.ShouldNotBeNullOrEmpty();
        payInResponse.Status.ShouldBe(TransactionStatus.CREATED);
        payInResponse.PaymentType.ShouldBe(PayInPaymentType.CARD);
        payInResponse.DebitedFunds.Amount.ShouldBe(debitedAmount);

        var creditedFunds = debitedAmount - payInResponse.Fees.Amount;
        payInResponse.CreditedFunds.Amount.ShouldBe(creditedFunds);
    }
}
