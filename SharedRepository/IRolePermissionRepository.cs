using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRepository
{
    public interface IRolePermissionRepository
    {
        Task<List<long>> GetPermissionIdsByRoleIds(List<long> roleIds);
    }
}
