using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Incoming.UserInsertDtoClass
{
    public class UserInsertDtoMapper : Profile
    {
        public UserInsertDtoMapper()
        {
            CreateMap<IUserInsert, UserInsertDto>();
        }
    }
}
