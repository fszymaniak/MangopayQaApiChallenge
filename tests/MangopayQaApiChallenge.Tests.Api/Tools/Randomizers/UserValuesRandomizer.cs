using MangopayQaApiChallenge.Tests.Api.Exceptions;

namespace MangopayQaApiChallenge.Tests.Api.Tools.Randomizers;

public class UserValuesRandomizer : IUserValuesRandomizer
{
    public string GetRandomValueFromTxtFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        int indicator = 0;
        var result = lines[Random.Shared.Next(indicator, lines.Length - 1)] ?? throw new EmptyRandomFileException(path);
        
        return result;
    }
}