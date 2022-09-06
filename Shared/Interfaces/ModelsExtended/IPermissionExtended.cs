using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IPermissionExtended
    {
        public ICollection<IRolePermission> RolePermissions { get; }
    }
}
