using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;

namespace Services.Models
{
    internal class ServicesPermission : IPermission, IPermissionExtended
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ServicesRolePermission> RolePermissions { get; set; }
        ICollection<IRolePermission> IPermissionExtended.RolePermissions => RolePermissions?.ToList<IRolePermission>();
    }
}
