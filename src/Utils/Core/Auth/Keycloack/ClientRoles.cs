using System.Text.Json.Serialization;

namespace Core.Auth.Keycloack;

public class ClientRoles
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}