using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRepository
{
    public interface IRoleRepository
    {
        Task<List<IRole>> GetAll();
        Task<bool> Insert(IRole role);
    }
}
