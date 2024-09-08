namespace MangopayQaApiChallenge.Tests.Api.Extensions;

public static class ExceptionAssertionExtension
{
    public static void ValidateType<T>(this Exception exception) => exception.GetType().ShouldBe(typeof(T));
}