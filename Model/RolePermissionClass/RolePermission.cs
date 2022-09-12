using Model.PermissionClass;
using Model.RoleClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.RolePermissionClass
{
    [Table("RolePermission")]
    public class RolePermission : IRolePermissionExtended
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }

        IRole IRolePermissionExtended.Role => Role;
    }
}
