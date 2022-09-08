using AutoMapper;
using Shared.Interfaces.Models;

namespace Model.UserClass
{
    public class UserAutoMapper : Profile
    {
        public UserAutoMapper()
        {
            CreateMap<IUser, User>();
            CreateMap<IUserInsert, User>();
            //.ForMember(dest => dest.UserRoles,
            //           opts => opts.MapFrom(src => src.Roles.Select(x => new UserRole() { RoleId = x })));
        }

    }
}
