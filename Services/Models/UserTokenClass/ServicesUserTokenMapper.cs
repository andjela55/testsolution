using AutoMapper;
using Shared.Interfaces.Models;

namespace Services.Models.UserTokenClass
{
    public class ServicesUserTokenMapper : Profile
    {
        public ServicesUserTokenMapper()
        {
            CreateMap<IUserToken, ServicesUserToken>();
        }
    }
}
