using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IUserRoleExtended : IUserRole
    {
        public IUser User { get; }
        public IRole Role { get; }
    }
}
