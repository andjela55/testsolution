using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IRolePermissionExtended : IRolePermission
    {
        public IRole Role { get; }
    }
}
