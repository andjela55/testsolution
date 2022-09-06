namespace Shared.Interfaces.Models
{
    public interface IUserInsert
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        IEnumerable<long> Roles { get; }
    }
}
