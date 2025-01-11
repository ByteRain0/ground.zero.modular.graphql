using System.Text.Json.Serialization;

namespace Core.Auth.Keycloak;

public class ClientRoles
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}