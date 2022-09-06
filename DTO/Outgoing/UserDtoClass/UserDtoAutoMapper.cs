using AutoMapper;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Outgoing.UserDtoClass
{
    public class UserDtoAutoMapper : Profile
    {
        public UserDtoAutoMapper()
        {
            CreateMap<IUser, UserDto>();
        }
    }
}
