namespace MangopayQaApiChallenge.Tests.Api.Exceptions;

public class EmptyLastRequestInfoException : CustomException
{
    public EmptyLastRequestInfoException() : base("Last Request Info does not return any data.")
    {
    }
}