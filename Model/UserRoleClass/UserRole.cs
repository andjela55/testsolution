using Model.RoleClass;
using Model.UserClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;

namespace Model
{
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
