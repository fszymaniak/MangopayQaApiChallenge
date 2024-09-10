namespace MangopayQaApiChallenge.Tests.Api.Exceptions;

public class EmptyRandomFileException : CustomException
{
    public EmptyRandomFileException(string path) : base($"File: {path} with random data is empty")
    {
    }
}