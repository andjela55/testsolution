using Model.RolePermissionClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.RoleClass
{

    [Table("Role")]
    public class Role : IRole, IRoleExtended
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        ICollection<IRolePermission> IRoleExtended.RolePermission => RolePermission?.ToList<IRolePermission>();

        ICollection<IUserRole> IRoleExtended.UserRoles => UserRoles?.ToList<IUserRole>();
    }
}
