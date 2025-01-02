using System.Diagnostics;

namespace Core.Otel;

public static class ActivityExtensions
{
    public static Activity? AddExceptionAndFail(this Activity? activity, Exception exception)
    {
        activity?.AddException(exception);
        activity?.SetStatus(ActivityStatusCode.Error);
        return activity;
    }
}