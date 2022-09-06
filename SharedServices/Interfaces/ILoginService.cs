using Shared.Interfaces.Models;

namespace SharedServices.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginUser(ILogin loginData);
        Task<bool> SetInitialPassword(ILogin data);
    }
}
