using Shared.Interfaces.Models;

namespace Services.Models
{
    public class ServicesRole : IRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ServicesRolePermission> RolePermission { get; set; }
        public ICollection<ServicesUserRole> UserRoles { get; set; }
    }
}
