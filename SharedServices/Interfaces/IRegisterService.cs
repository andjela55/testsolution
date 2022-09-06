using Shared.Interfaces.Models;

namespace SharedServices.Interfaces
{
    public interface IRegisterService
    {
        public Task RegisterUser(IUserInsert userData);
    }
}
