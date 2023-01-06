using Shared.Interfaces.Models;

namespace DTO.Outgoing
{
    public class LoginResponseDto : ILoginResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
