namespace Permitt.Services
{
    public class UserPermittedForEntityRequest : PermissionRequestBase
    {
        public string EntityId { get; set; }
    }
}