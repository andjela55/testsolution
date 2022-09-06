using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PermissionClass
{
    public  class PermissionAutoMapper : Profile
    {
        public PermissionAutoMapper()
        {
            CreateMap<IPermission, Permission>();
        }
    }
}
