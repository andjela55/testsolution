namespace SharedServices
{
    public interface IHashService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt, int nIterations, int nHash);
    }
}
