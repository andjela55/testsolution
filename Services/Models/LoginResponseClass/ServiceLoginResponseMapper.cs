using AutoMapper;
using Shared.Interfaces.Models;

namespace Services.Models.LoginResponseClass
{
    internal class ServiceLoginResponseMapper : Profile
    {
        public ServiceLoginResponseMapper()
        {
            CreateMap<ILoginResponse, ServiceLoginResponse>();
        }
    }
}
