using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Permitt.Services
{
    public class DefaultPermissionService : IPermissionService
    {
        private readonly IPermissionRecordStore _permissions;
        public DefaultPermissionService(IPermissionRecordStore permissions)
        {
            _permissions = permissions;
        }
        public async Task<UserPermittedForActionResponse> UserIsPermittedForAction(UserPermittedForActionRequest request)
        {
            ThrowIfAnyIsNull(request);
            var p = await _permissions.GetUserPermissionRecordForActionAsync(
                request.UserId,
                request.PermissionType,
                request.EntityName);
            var res = new UserPermittedForActionResponse(request)
            {
                Permitted = !p.IsNullOrEmpty() && p.Any(x => x.Active)
            };
            return res;
        }
        public async Task<UserPermittedForEntityResponse> UserIsPermittedForEntityAsync(UserPermittedForEntityRequest request)
        {
            ThrowIfAnyIsNull(request);
            if (!request.EntityId.HasValue()) throw new ArgumentNullException(nameof(request.EntityId));

            var p = await _permissions.GetUserPermissionRecordForEntityAsync(
                request.UserId,
                request.PermissionType,
                request.EntityName,
                request.EntityId);
            var res = new UserPermittedForEntityResponse(request)
            {
                Permitted = !p.IsNullOrEmpty() && p.Any(x => x.Active)
            };
            return res;
        }
        private void ThrowIfAnyIsNull(PermissionRequestBase request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (!request.UserId.HasValue()) throw new ArgumentNullException(nameof(request.UserId));
            if (!request.EntityName.HasValue()) throw new ArgumentNullException(nameof(request.EntityName));
            if (!request.PermissionType.HasValue()) throw new ArgumentNullException(nameof(request.PermissionType));
        }
    }
}