using System.Diagnostics;
using Core.Otel;
using HotChocolate;

namespace Core.Extensions;

public static class ErrorBuilderExtensions
{
    public static IErrorBuilder SetTraceIdentifiers(this IErrorBuilder builder)
    {
        var currentActivity = Activity.Current;

        if (currentActivity != null)
        {
            builder
                .SetExtension(GlobalOTelTags.TraceId, currentActivity.TraceId)
                .SetExtension(GlobalOTelTags.SpanId, currentActivity.SpanId);
        }

        return builder;
    }
}