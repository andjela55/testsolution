namespace Shared.Interfaces.Models
{
    public interface IUserInsert
    {
        public long Id { get; set; }
        public string Name { get; }

        public string Email { get; }

        public string Password { get; }
        public IEnumerable<long> Roles { get; }
    }
}
