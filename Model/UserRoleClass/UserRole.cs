using Model.RoleClass;
using Model.UserClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("UserRole")]
    public class UserRole : IUserRole, IUserRoleExtended
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        IUser IUserRoleExtended.User => User;
        IRole IUserRoleExtended.Role => Role;

    }
}
