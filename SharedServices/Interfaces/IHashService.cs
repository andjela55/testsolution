namespace SharedServices
{
    public interface IHashService
    {
        string GenerateRandomString();
        string HashPassword(string password, string salt, int nIterations, int nHash);
    }
}
