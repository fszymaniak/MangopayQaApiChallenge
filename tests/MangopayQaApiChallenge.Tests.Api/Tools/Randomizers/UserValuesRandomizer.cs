using MangopayQaApiChallenge.Tests.Api.Exceptions;

namespace MangopayQaApiChallenge.Tests.Api.Tools.Randomizers;

public class UserValuesRandomizer : IUserValuesRandomizer
{
    private const int MinimumLineIndex = 0;

    public string GetRandomValueFromTxtFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        // Random.Shared.Next(min, max) is exclusive of max, so we use lines.Length to include all lines
        var result = lines[Random.Shared.Next(MinimumLineIndex, lines.Length)] ?? throw new EmptyRandomFileException(path);

        return result;
    }
}