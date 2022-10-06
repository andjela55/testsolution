using Model.RoleClass;
using Model.UserClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Table("UserRole")]
    public class UserRole : BaseEntity, IUserRoleExtended
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
        IUser IUserRoleExtended.User => User;
        IRole IUserRoleExtended.Role => Role;

    }
}
