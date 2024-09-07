namespace MangopayQaApiChallenge.Tests.Api.Tools.Providers;

public interface IPathProvider
{
    public string GetFilePath(string directory, string fileName);
}