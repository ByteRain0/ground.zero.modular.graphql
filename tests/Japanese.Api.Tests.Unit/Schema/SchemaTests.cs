using CookieCrumble;
using HotChocolate.Execution;
using Japanese.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Japanese.Api.Tests.Unit.Schema;

public class SchemaTests
{
    [Fact(Skip = "Fix the issues when dealing with the testing aspect of the system.")]
    public async Task SchemaChanged()
    {
        var builder = WebApplication
            .CreateBuilder();

        builder
            .Configuration
            .SetBasePath(Path.Combine(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.DynamicEntities.json", optional: false, reloadOnChange: false);


        var schema = await builder
             .AddApplicationServices()
             .Services.AddGraphQLInfrastructure()
             .BuildSchemaAsync();

        schema.MatchSnapshot();
    }
}