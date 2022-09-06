namespace Shared.Interfaces.Models
{
    public interface IUser
    {
        long Id { get; }
        string Name { get; }
        string Password { get; }
        string Email { get; }
        string Salt { get; }
        public bool AccountConfirmed { get; }
    }
}
