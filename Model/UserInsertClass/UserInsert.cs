using Shared.Interfaces.Models;

namespace Model.UserInsertClass
{
    public class UserInsert : IUserInsert
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
        public IEnumerable<long> Roles { get; set; }
    }
}
