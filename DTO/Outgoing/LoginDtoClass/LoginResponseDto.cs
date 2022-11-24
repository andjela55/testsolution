using Shared.Interfaces.Models;

namespace DTO.Outgoing.LoginDtoClass
{
    public class LoginResponseDto : ILoginResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
