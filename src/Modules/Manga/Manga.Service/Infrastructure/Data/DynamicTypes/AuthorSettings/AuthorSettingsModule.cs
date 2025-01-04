using System.Text.Json;
using Core.DynamicEntities;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.Extensions.Configuration;
using Manga.Contracts.Models;
using Microsoft.Extensions.Primitives;

namespace Manga.Service.Infrastructure.Data.DynamicTypes.AuthorSettings;

public class AuthorSettingsModule : ITypeModule
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _settingsSection;

    public AuthorSettingsModule(IConfiguration configuration)
    {
        _configuration = configuration;
        _settingsSection = _configuration.GetSection("AuthorSettings");

        // Monitor changes in the appsettings.json file
        ChangeToken.OnChange(
            () => _settingsSection.GetReloadToken(),
            () => TypesChanged?.Invoke(this, EventArgs.Empty));
    }

    public event EventHandler<EventArgs>? TypesChanged;

    public ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(
        IDescriptorContext context,
        CancellationToken cancellationToken)
    {
        var types = new List<ITypeSystemMember>();

        // Get settings from appsettings.json
        if (!_settingsSection.Exists())
        {
            throw new InvalidOperationException("AuthorSettings section is missing in appsettings.json.");
        }

        var typeConfigs = _settingsSection.Get<List<TypeConfig>>() ?? new List<TypeConfig>();

        foreach (var typeConfig in typeConfigs)
        {
            var type = new ObjectTypeDefinition(typeConfig.Name);

            foreach (var fieldConfig in typeConfig.Fields)
            {
                var field = new ObjectFieldDefinition(
                    name: fieldConfig.Name,
                    type: TypeReference.Parse(fieldConfig.Type)!);

                var fieldDesc = ObjectFieldDescriptor.From(context, field);

                fieldDesc.FromJson();
                fieldDesc.CreateDefinition();

                type.Fields.Add(field);
            }

            types.Add(ObjectType.CreateUnsafe(type));
        }

        var settingsType = TypeReference.Create(types[0]);

        var author = new ObjectTypeExtension<Author>(d =>
        {
            d.Field(x => x.Settings)
                .Resolve(ctx =>
                {
                    if (ctx.Parent<Author>().Settings is string s)
                    {
                        return JsonDocument.Parse(s).RootElement;
                    }

                    return null;
                })
                .Extend()
                .Definition.Type = settingsType;
        });
        
        types.Add(author);

        return new ValueTask<IReadOnlyCollection<ITypeSystemMember>>(types);
    }
}