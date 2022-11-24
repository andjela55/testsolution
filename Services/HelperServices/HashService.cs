using Microsoft.Extensions.Configuration;
using SharedServices;
using System.Security.Cryptography;

namespace Services.HelperServices
{
    public class HashService : IHashService
    {
        private IConfiguration _config;
        public HashService(IConfiguration config)
        {
            _config = config;

        }
        public string GenerateRandomString()
        {
            var randomNumber = new byte[32];
            string salt = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                salt = Convert.ToBase64String(randomNumber);
            }
            return salt;
        }
        public string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }
    }
}
