using System.Collections.Generic;
using System.Threading.Tasks;

namespace Permitt.Services
{
    public interface IPermissionRecordStore
    {
        Task<IEnumerable<PermissionRecord>> GetUserPermissionRecordForActionAsync(string userId, string permissionType, string entityName);
        Task<IEnumerable<PermissionRecord>> GetUserPermissionRecordForEntityAsync(string userId, string permissionType, string entityName, string entityId);
    }
}
