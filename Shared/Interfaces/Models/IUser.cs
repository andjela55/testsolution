namespace Shared.Interfaces.Models
{
    public interface IUser
    {
        public long Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }
        public string Salt { get; }
        public bool AccountConfirmed { get; }
    }
}
