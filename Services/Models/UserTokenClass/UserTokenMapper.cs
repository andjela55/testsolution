using AutoMapper;
using Shared.Interfaces.Models;

namespace Services.Models.UserTokenClass
{
    public class UserTokenMapper : Profile
    {
        public UserTokenMapper()
        {
            CreateMap<IUserToken, UserToken>();
        }
    }
}
