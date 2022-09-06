using Shared.Enums;

namespace Shared.Interfaces.Models
{
    public interface IUserToken
    {
        public long Id { get; }
        public string Token { get; }
        public DateTime ExpirationDate { get; }
        public long UserId { get; }
        public bool IsUsed { get; }
        public TokenType TokenType { get; }
    }
}
