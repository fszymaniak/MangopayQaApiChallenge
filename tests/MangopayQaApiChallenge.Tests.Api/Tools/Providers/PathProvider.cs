namespace MangopayQaApiChallenge.Tests.Api.Tools.Providers;

public class PathProvider : IPathProvider
{
    public string GetFilePath(string directory, string fileName) => Path.Combine(directory, fileName);
}