namespace MangopayQaApiChallenge.Tests.Api.Helpers;

public static class ExceptionAssertions
{
    public static async Task<T> ShouldThrowWithMessageAsync<T, TException>(
        Func<Task<T>> action,
        string expectedMessagePart)
        where TException : Exception
    {
        T result = default!;
        try
        {
            result = await action();
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(TException));
            exception.Message.ShouldContain(expectedMessagePart);
        }

        return result;
    }

    public static async Task ShouldThrowWithMessageAsync<TException>(
        Func<Task> action,
        string expectedMessagePart)
        where TException : Exception
    {
        try
        {
            await action();
        }
        catch (Exception exception)
        {
            exception.GetType().ShouldBe(typeof(TException));
            exception.Message.ShouldContain(expectedMessagePart);
        }
    }
}
