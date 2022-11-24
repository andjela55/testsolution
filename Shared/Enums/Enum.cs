namespace Shared.Enums
{
    public enum PermissionTypes
    {
        RoleAdmin = 1,
        CanViewAllUsers = 2,
    }
    public enum TokenType
    {
        VerificationToken = 1,
        PasswordResetToken,
        RefreshToken
    }
}
