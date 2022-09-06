using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserInsertClass
{
    
    public class UserInsertAutoMapper : Profile
    {
        public UserInsertAutoMapper()
        {
            CreateMap<IUserInsert, UserInsert>();
        }

    }
}
