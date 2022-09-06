using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServices.Interfaces
{
    public interface IUserService
    {
        Task<IUser> GetCurrentUser();
        Task<List<IUser>> GetAll();
        Task<bool> Insert(IUserInsert user);
        Task<IUser> GetById(long id);
    }
}
