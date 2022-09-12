using Model.RolePermissionClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.PermissionClass
{
    [Table("Permission")]
    public class Permission : BaseEntity, IPermissionExtended
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        ICollection<IRolePermission> IPermissionExtended.RolePermissions => RolePermissions?.ToList<IRolePermission>();
    }
}
