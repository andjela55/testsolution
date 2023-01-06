using Shared.Interfaces.Models;

namespace SharedServices.Interfaces
{
    public interface IUserService
    {
        Task<IUser> GetCurrentUser();
        Task<List<IUser>> GetAll();
        Task<bool> Insert(IUserInsert user);
        Task<IUser> GetById(long id);
        Task<bool> Update(IUserInsert user);
        Task<IUser> GetByEmail(string email);
        Task<List<IUser>> GetByIds(IEnumerable<long> ids);
    }
}
