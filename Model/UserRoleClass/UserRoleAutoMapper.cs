using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserRoleClass
{
    public partial class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IUserRole, UserRole>();
        }

    }
}
