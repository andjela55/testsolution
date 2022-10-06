

using Model.UserClass;
using Shared.Enums;
using Shared.Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.UserTokenClass
{
    [Table("UserToken")]
    public class UserToken : BaseEntity, IUserToken
    {
        [Key]
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long UserId { get; set; }
        public bool IsUsed { get; set; }
        public TokenType TokenType { get; set; }
        public virtual User User { get; set; }
    }
}
