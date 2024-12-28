namespace Core.Environment;

/// <summary>
/// Simple static class that can be used across the solution to determine the env.
/// </summary>
public static class AppHost
{
    public static bool IsDevelopment()
    {
        var currentEnv = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (currentEnv is null)
        {
            return false;
        }

        return currentEnv.Equals("development", StringComparison.CurrentCultureIgnoreCase);
    }
}