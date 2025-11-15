namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class PayInFactory : IPayInFactory
{
    private readonly IUserValuesRandomizer _userValuesRandomizer;
    private readonly string _randomFilesDirectory = FilesDirectories.RandomDirectory;

    public PayInFactory(IUserValuesRandomizer userValuesRandomizer)
    {
        _userValuesRandomizer = userValuesRandomizer;
    }
    
    public PayInCardDirectPostDTO CreateValidDirectPayIn(UserNaturalDTO userNatural, WalletDTO wallet,
        CardRegistrationDTO cardRegistration, int amount, CurrencyIso currency)
    {
        var debitedFounds = new Money
        {
            Amount = amount,
            Currency = currency
        };

        var fees = new Money
        {
            Amount = PayInConstants.DefaultFeeAmount,
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
            AcceptHeader = HeaderConstants.AcceptHeader,
            JavaEnabled = true,
            Language = BrowserInfoConstants.Language,
            ColorDepth = BrowserInfoConstants.ColorDepth,
            ScreenHeight = BrowserInfoConstants.ScreenHeight,
            ScreenWidth = BrowserInfoConstants.ScreenWidth,
            TimeZoneOffset = BrowserInfoConstants.TimeZoneOffset,
            UserAgent = BrowserInfoConstants.UserAgent,
            JavascriptEnabled = true
        };
        
        var payInCardDirectPostDTO =  new PayInCardDirectPostDTO
        (
            authorId: userNatural.Id,
            creditedUserId: userNatural.Id,
            debitedFunds: debitedFounds,
            fees: fees,
            creditedWalletId: wallet.Id,
            secureModeReturnURL: PayInCardDirectPostConstants.SecureModeReturnUrl,
            cardId: cardRegistration.CardId,
            statementDescriptor: PayInCardDirectPostConstants.StatementDescriptor,
            billing: billing,
            bInfo: browserInfo
        );

        payInCardDirectPostDTO.IpAddress =
            _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomIpv6AddressesTxt));

        return payInCardDirectPostDTO;
    }
}