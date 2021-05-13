namespace Permitt.Services
{
    public abstract class PermissionRequestBase
    {
        public string UserId { get; set; }
        public string EntityName { get; set; }
        public string PermissionType { get; set; }
    }
}