using HotChocolate;

namespace Core.Extensions;

public static class OptionalExtensions
{
    public static void UpdateIfHasValue<TProperty>(
        this TProperty targetProperty,
        Optional<TProperty> optional,
        Action<TProperty> updateAction,
        Action? onUpdated = null)
    {
        if (optional.HasValue)
        {
            updateAction(optional.Value);
            onUpdated?.Invoke();
        }
    }
}