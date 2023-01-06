using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserRepository
    {
        Task<IUser> AuthenticateUser(string email, string password);
        Task<IUser> GetById(long userId);
        Task<List<IUser>> GetAllUsers();
        Task<IUser> Create(IUser user);
        Task<IUser> GetByEmail(string email);
        Task<bool> UpdateUser(IUser userForUpdate);
        Task<List<IUser>> GetUsersByIds(IEnumerable<long> ids);
    }
}
