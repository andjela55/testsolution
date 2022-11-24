using Shared.Interfaces.Models;

namespace SharedRepository
{
    public interface IUserTokenRepository
    {
        public Task<bool> Create(IUserToken user);
        public Task<IUserToken> GetRegistrationTokenForUser(long id);
        public Task<bool> Update(long id, IUserToken tokenForUpdate);
        public Task<bool> DeleteExpiredTokens();
        public Task<IUserToken> GetRefreshTokenByValue(string refreshToken);
        public Task SetUsedRefreshToken(IUserToken refreshToken);
    }
}
