using Shared.Interfaces.Models;

namespace Services.Models
{
    public class UserRole : IUserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
