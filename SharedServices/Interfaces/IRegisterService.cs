using Shared.Interfaces.Models;

namespace SharedServices.Interfaces
{
    public interface IRegisterService
    {
        public Task RegisterUser(IUserInsert userData);
        public Task<bool> ConfirmRegistration(long id, string token);
    }
}
