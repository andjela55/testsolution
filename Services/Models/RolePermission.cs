using Shared.Interfaces.Models;

namespace Services.Models
{
    public class RolePermission : IRolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public Role Role { get; set; }
    }
}
