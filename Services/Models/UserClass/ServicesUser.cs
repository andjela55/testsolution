using Services.Models.UserTokenClass;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;

namespace Services.Models.UserClass
{
    public class ServicesUser : IUser
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }

        public bool AccountConfirmed { get; set; }

        //public virtual ICollection<ServicesUserRole> UserRoles { get; set; }

        //public virtual ICollection<ServicesUserToken> UserTokens { get; set; }

    }
}
