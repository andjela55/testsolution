using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserTokenRepository
    {
        public Task<bool> Insert(IUserToken user);
        public Task<IUserToken> GetRegistrationTokenForUser(long id);
        public Task<bool> Update(long id, IUserToken tokenForUpdate);
        public Task<bool> DeleteExpiredTokens();
    }
}
