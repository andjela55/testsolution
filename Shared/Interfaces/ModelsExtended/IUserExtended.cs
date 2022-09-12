using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IUserExtended : IUser
    {
        ICollection<IUserRole> UserRoles { get; }
    }
}
