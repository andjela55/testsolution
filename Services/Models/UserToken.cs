using Shared.Enums;
using Shared.Interfaces.Models;

namespace Services.Models
{
    public class UserToken : IUserToken
    {
        public long Id { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public long UserId { get; set; }

        public bool IsUsed { get; set; }

        public TokenType TokenType { get; set; }
    }
}
