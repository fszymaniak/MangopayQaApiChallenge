namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class UserFactory : IUserFactory
{
    private readonly IUserValuesRandomizer _userValuesRandomizer;
    private readonly string _randomFilesDirectory = FilesDirectories.RandomDirectory;

    public UserFactory(IUserValuesRandomizer userValuesRandomizer)
    {
        _userValuesRandomizer = userValuesRandomizer;
    }

    public UserNaturalPayerPostDTO CreateValidUser()
    {
        return new UserNaturalPayerPostDTO
        {
            FirstName = _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomFirstNamesTxt)),
            LastName = _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomLastNamesTxt)),
            Email = _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomEmailsTxt)),
            Address = new Address
            {
                AddressLine1 = _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomStreetAddressTxt)),
                AddressLine2 = _userValuesRandomizer.GetRandomValueFromTxtFile(Path.Combine(_randomFilesDirectory, RandomFileNames.RandomStreetNamesTxt)),
                City =  AddressConstants.City,
                Region = AddressConstants.Region,
                PostalCode = AddressConstants.PostalCode,
                Country = AddressConstants.Country
            },
            UserCategory = UserCategory.PAYER,
            TermsAndConditionsAccepted = true,
            Tag = TestDataConstants.UserTag
        };
    }
}