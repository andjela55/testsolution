using Shared.Interfaces.Models;

namespace Services.Models
{
    public class ServicesRolePermission : IRolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public ServicesRole Role { get; set; }
    }
}
