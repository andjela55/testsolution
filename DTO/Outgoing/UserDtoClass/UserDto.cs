using Shared.Interfaces.Models;

namespace DTO.Outgoing
{
    public class UserDto : IUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool AccountConfirmed { get; set; }
    }
}
