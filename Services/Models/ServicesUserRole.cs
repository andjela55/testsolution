using Services.Models.UserClass;
using Shared.Interfaces.Models;

namespace Services.Models
{
    public class ServicesUserRole : IUserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual ServicesUser User { get; set; }
        public virtual ServicesRole Role { get; set; }
    }
}
