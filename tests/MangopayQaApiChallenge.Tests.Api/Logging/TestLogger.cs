using Common.Logging;
using Common.Logging.Simple;

namespace MangopayQaApiChallenge.Tests.Api.Logging;

public static class TestLogger
{
    private static readonly ILog _log;

    static TestLogger()
    {
        // Configure Common.Logging to use console logger
        var properties = new Common.Logging.Configuration.NameValueCollection
        {
            ["level"] = "INFO",
            ["showLogName"] = "true",
            ["showDateTime"] = "true",
            ["dateTimeFormat"] = "yyyy-MM-dd HH:mm:ss"
        };

        LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(properties);
        _log = LogManager.GetLogger("MangopayQaApi");
    }

    public static void Info(string message) => _log.Info(message);

    public static void Debug(string message) => _log.Debug(message);

    public static void Warn(string message) => _log.Warn(message);

    public static void Error(string message) => _log.Error(message);

    public static void Error(string message, Exception exception) => _log.Error(message, exception);

    public static void ApiRequest(string method, string endpoint)
    {
        _log.Info($"[API REQUEST] {method} {endpoint}");
    }

    public static void ApiResponse(string method, string endpoint, int statusCode)
    {
        _log.Info($"[API RESPONSE] {method} {endpoint} - Status: {statusCode}");
    }

    public static void TestStart(string testName)
    {
        _log.Info($"[TEST START] {testName}");
    }

    public static void TestEnd(string testName, string result)
    {
        _log.Info($"[TEST END] {testName} - {result}");
    }

    public static void TestSetup(string message)
    {
        _log.Info($"[SETUP] {message}");
    }

    public static void TestTearDown(string message)
    {
        _log.Info($"[TEARDOWN] {message}");
    }
}
