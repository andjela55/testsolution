using Shared.Interfaces.Models;

namespace SharedServices.Interfaces
{
    public interface ILoginService
    {
        Task<ILoginResponse> LoginUser(ILogin loginData);
        Task<bool> SetInitialPassword(ILogin data);
        Task<ILoginResponse> RefreshTokens(string refreshToken);
    }
}
