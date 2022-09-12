using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IRoleExtended : IRole
    {
        public ICollection<IRolePermission> RolePermission { get; }
        public ICollection<IUserRole> UserRoles { get; }

    }
}
