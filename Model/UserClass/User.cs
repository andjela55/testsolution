using Model.UserTokenClass;
using Newtonsoft.Json;
using Shared.Interfaces.Models;
using Shared.Interfaces.ModelsExtended;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Model.UserClass
{
    [Table("User")]
    public class User : IUser, IUserExtended
    {
        [Required]
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }

        public bool AccountConfirmed { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

        [NotMapped]
        ICollection<IUserRole> IUserExtended.UserRoles => UserRoles?.ToList<IUserRole>();
    }
}
