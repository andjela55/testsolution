using AutoMapper;
using Shared.Interfaces.Models;

namespace DTO.Outgoing.LoginDtoClass
{
    public class LoginDtoMapper : Profile
    {
        public LoginDtoMapper()
        {
            CreateMap<ILoginResponse, LoginResponseDto>();
        }
    }
}
