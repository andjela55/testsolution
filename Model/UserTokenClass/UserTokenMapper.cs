using AutoMapper;
using Shared.Interfaces.Models;

namespace Model.UserTokenClass
{
    public class UserTokenMapper : Profile
    {
        public UserTokenMapper()
        {
            CreateMap<IUserToken, UserToken>();
        }
    }
}
