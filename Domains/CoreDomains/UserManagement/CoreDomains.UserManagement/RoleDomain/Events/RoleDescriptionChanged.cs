namespace CoreDomains.UserManagement.RoleDomain.Events
{
    public interface RoleDescriptionChanged : IRoleEvent
    {
        string Description { get; }
    }
}