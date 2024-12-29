// using Anime.GraphQL.Infrastructure;
// using Anime.Service.Infrastructure;
// using CookieCrumble;
// using HotChocolate.Execution;
// using Japanese.Api.Infrastructure;
// using Manga.GraphQL.Infrastructure;
// using Manga.Service.Infrastructure;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Japanese.Api.Tests.Unit.Schema;
//
// public class SchemaTests
// {
//     [Fact]
//     public async Task SchemaChanged()
//     {
//         var basePath = Path.Combine(AppContext.BaseDirectory);
//         
//         var configuration = new ConfigurationBuilder()
//             .SetBasePath(basePath)
//             .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: false)
//             .Build();
//         
//         var schema = await new ServiceCollection()
//             .AddApplicationServices(configuration)
//             .AddGraphQLInfrastructure()
//             .BuildSchemaAsync();
//         
//         schema.MatchSnapshot();
//     }
// }