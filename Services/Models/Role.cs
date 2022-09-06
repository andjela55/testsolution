using Shared.Interfaces.Models;

namespace Services.Models
{
    public class Role : IRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
