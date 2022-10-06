using Model.RolePermissionClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations;

namespace Model.RoleClass
{
    public class Role : BaseEntity, IRoleExtended
    {
        [Required]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        ICollection<IRolePermission> IRoleExtended.RolePermission => RolePermission?.ToList<IRolePermission>();

        ICollection<IUserRole> IRoleExtended.UserRoles => UserRoles?.ToList<IUserRole>();

    }
}
