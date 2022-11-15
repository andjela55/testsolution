using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserRoleRepository
    {
        Task<List<long>> GetUserRolesIdsByUserId(long userId);
        Task<bool> AddForUser(long userId, IEnumerable<long> roles);
        Task<IUserRole> Create(IUserRole userRole);
    }
}
