using System.Threading.Tasks;

namespace Permitt.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Gets user's permissions for specific entity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserPermittedForEntityResponse> UserIsPermittedForEntityAsync(UserPermittedForEntityRequest request);
        /// <summary>
        /// Get's users permission for action
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserPermittedForActionResponse> UserIsPermittedForAction(UserPermittedForActionRequest request);
    }
}
