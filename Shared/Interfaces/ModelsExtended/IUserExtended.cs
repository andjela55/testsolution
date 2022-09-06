using Shared.Interfaces.Models;

namespace Shared.Interfaces.ModelsExtended
{
    public interface IUserExtended
    {
        ICollection<IUserRole> UserRoles { get; }
    }
}
