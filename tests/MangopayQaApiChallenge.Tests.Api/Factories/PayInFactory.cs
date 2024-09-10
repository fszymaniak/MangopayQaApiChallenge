using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class PayInFactory : IPayInFactory
{
    private readonly IPathProvider _pathProvider;
    private readonly IUserValuesRandomizer _userValuesRandomizer;
    private readonly string _randomFilesDirectory = FilesDirectories.RandomDirectory;

    public PayInFactory(IPathProvider pathProvider, IUserValuesRandomizer userValuesRandomizer)
    {
        _pathProvider = new PathProvider();
        _userValuesRandomizer = new UserValuesRandomizer();
    }
    
    public PayInCardDirectPostDTO CreateValidDirectPayIn(UserNaturalDTO userNatural, WalletDTO wallet,
        CardRegistrationDTO cardRegistration, int amount, CurrencyIso currency)
    {
        var debitedFounds = new Money
        {
            Amount = 10000,
            Currency = currency
        };

        var fees = new Money
        {
            Amount = 100,
            Currency = currency
        };

        var billing = new Billing
        {
            FirstName = userNatural.FirstName,
            LastName = userNatural.LastName,
            Address = userNatural.Address
        };

        var browserInfo = new BrowserInfo
        {
            AcceptHeader = "text/html, application/xhtml+xml, application/xml;q=0.9, /;q=0.8",
            JavaEnabled = true,
            Language = "en",
            ColorDepth = 4,
            ScreenHeight = 1800,
            ScreenWidth = 400,
            TimeZoneOffset = "60",
            UserAgent =
                "Mozilla/5.0 (iPhone; CPU iPhone OS 13_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148",
            JavascriptEnabled = true
        };
        
        var payInCardDirectPostDTO =  new PayInCardDirectPostDTO
        (
            authorId: userNatural.Id,
            creditedUserId: userNatural.Id,
            debitedFunds: debitedFounds,
            fees: fees,
            creditedWalletId: wallet.Id,
            secureModeReturnURL: "https://mangopay.com/docs/please-ignore",
            cardId: cardRegistration.CardId,
            statementDescriptor: "Mangopay",
            billing: billing,
            bInfo: browserInfo
        );

        payInCardDirectPostDTO.IpAddress =
            _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomIpv6AddressesTxt));

        return payInCardDirectPostDTO;
    }
    
    private string GetPath(string fileName)
    {
        return _pathProvider.GetFilePath(_randomFilesDirectory, fileName);
    }
}