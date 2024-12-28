namespace Core.Auth;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class AuthorizeRolesAttribute : Attribute
{
    public string[] Roles { get; }

    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = roles;
    }
}