using Shared.Interfaces.Models;

namespace Services.Models.LoginResponseClass
{
    public class ServiceLoginResponse : ILoginResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
