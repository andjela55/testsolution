using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using SharedRepository;

namespace Repository
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private Context _context;

        public RolePermissionRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<long>> GetPermissionIdsByRoleIds(List<long> roleIds)
        {
            var permissionIds = await _context.RolePermissions.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.PermissionId).ToListAsync();
            return permissionIds;
        }
    }
}
