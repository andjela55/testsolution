using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserRepository
    {
        Task<IUser> AuthenticateUser(string email, string password);
        Task<IUser> GetById(long userId);
        Task<List<IUser>> GetAll();
        Task<bool> Insert(IUser user);
        Task<IUser> GetByEmail(string email);
        Task<bool> Update(long idUser, IUser userForUpdate);
    }
}
