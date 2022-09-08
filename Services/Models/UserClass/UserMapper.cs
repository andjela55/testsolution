using AutoMapper;
using Shared.Interfaces.Models;

namespace Services.Models.UserClass
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<IUser, User>();
            CreateMap<IUserInsert, User>()
                     .ForMember(dest => dest.UserRoles,
                                opts => opts.MapFrom(src => src.Roles.Select(x => new UserRole() { RoleId = x })));
        }
    }
}
