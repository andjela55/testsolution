using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRepository
{
    public interface IUserRoleRepository
    {
        Task<List<long>> GetUserRolesIdsByUserId(long userId);
    }
}
