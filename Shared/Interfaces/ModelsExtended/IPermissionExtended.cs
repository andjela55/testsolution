using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IPermissionExtended : IPermission
    {
        public ICollection<IRolePermission> RolePermissions { get; }
    }
}
