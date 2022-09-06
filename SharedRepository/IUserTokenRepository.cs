using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserTokenRepository
    {
        Task<bool> Insert(IUserToken user);
    }
}
