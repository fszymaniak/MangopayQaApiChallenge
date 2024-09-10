using MangoPay.SDK.Core.Enumerations;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public interface IPayInFactory
{
    public PayInCardDirectPostDTO CreateValidDirectPayIn(UserNaturalDTO userNatural, WalletDTO wallet, CardRegistrationDTO cardRegistration, int amount, CurrencyIso currency);
}