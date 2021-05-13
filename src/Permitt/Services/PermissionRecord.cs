namespace Permitt.Services
{
    public class PermissionRecord
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string EntityName { get; set; }
        public string PermissionType { get; set; }
        public bool Active { get; set; }
    }
}
