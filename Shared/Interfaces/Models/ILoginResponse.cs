namespace Shared.Interfaces.Models
{
    public interface ILoginResponse
    {
        public string JwtToken { get; }
        public string RefreshToken { get; }
    }
}
