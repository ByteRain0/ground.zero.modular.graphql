namespace Core.DynamicEntities;

// Helper classes to map configuration
public class TypeConfig
{
    public string Name { get; set; } = string.Empty;
    public List<FieldConfig> Fields { get; set; } = new();
}