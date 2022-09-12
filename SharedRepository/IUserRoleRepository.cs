namespace SharedRepository
{
    public interface IUserRoleRepository
    {
        Task<List<long>> GetUserRolesIdsByUserId(long userId);
        Task<bool> AddRolesForUser(long userId, IEnumerable<long> roles);
    }
}
