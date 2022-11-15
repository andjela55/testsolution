using Shared.Interfaces.Models;

namespace DTO.Incoming
{
    public class UserInsertDto : IUserInsert
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<long> Roles { get; set; }

    }
}
