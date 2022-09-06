using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.RolePermissionClass
{
    public class RolePermissionAutoMapper : Profile
    {
        public RolePermissionAutoMapper()
        {
            CreateMap<IRolePermission, RolePermission>();
        }

    }
}