using MangoPay.SDK.Core.Enumerations;
using MangoPay.SDK.Entities;
using MangoPay.SDK.Entities.POST;
using MangopayQaApiChallenge.Tests.Api.Constants;
using MangopayQaApiChallenge.Tests.Api.Tools.Providers;
using MangopayQaApiChallenge.Tests.Api.Tools.Randomizers;

namespace MangopayQaApiChallenge.Tests.Api.Factories;

public class UserFactory : IUserFactory
{
    private readonly IPathProvider _pathProvider;
    private readonly IUserValuesRandomizer _userValuesRandomizer;
    private readonly string _randomFilesDirectory = FilesDirectories.RandomDirectory;

    public UserFactory(IPathProvider pathProvider, IUserValuesRandomizer userValuesRandomizer)
    {
        _pathProvider = new PathProvider();
        _userValuesRandomizer = new UserValuesRandomizer();
    }

    public UserNaturalPayerPostDTO CreateValidUser()
    {
        return new UserNaturalPayerPostDTO
        {
            FirstName = _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomFirstNamesTxt)),
            LastName = _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomLastNamesTxt)),
            Email = _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomEmailsTxt)),
            Address = new Address
            {
                AddressLine1 = _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomStreetAddressTxt)),
                AddressLine2 = _userValuesRandomizer.GetRandomValueFromTxtFile(GetPath(RandomFileNames.RandomStreetNamesTxt)),
                City =  AddressConstants.City,
                Region = AddressConstants.Region,
                PostalCode = AddressConstants.PostalCode,
                Country = AddressConstants.Country
            },
            UserCategory = UserCategory.PAYER,
            TermsAndConditionsAccepted = true,
            Tag = "Created using automated tests QA Challenge"
        };
    }

    private string GetPath(string fileName)
    {
        return _pathProvider.GetFilePath(_randomFilesDirectory, fileName);
    }
}