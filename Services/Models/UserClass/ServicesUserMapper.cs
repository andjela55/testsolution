using AutoMapper;
using Model.UserInsertClass;
using Shared.Interfaces.Models;

namespace Services.Models.UserClass
{
    public class ServicesUserMapper : Profile
    {
        public ServicesUserMapper()
        {
            CreateMap<IUser, ServicesUser>();
            CreateMap<IUserInsert, ServicesUser>();
        }
    }
}
