using Shared.Interfaces.Models;

namespace Services.Models
{
    public class User : IUser
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }

        public bool AccountConfirmed { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

    }
}
