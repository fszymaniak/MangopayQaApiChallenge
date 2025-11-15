namespace MangopayQaApiChallenge.Tests.Api.Exceptions;

public class EmptyLastRequestInfoException : CustomException
{
    public EmptyLastRequestInfoException() : base(ExceptionMessages.EmptyLastRequestInfo)
    {
    }
}