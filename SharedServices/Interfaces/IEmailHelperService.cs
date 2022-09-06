namespace SharedServices.Interfaces
{
    public interface IEmailHelperService
    {
        public bool SendEmail(string userEmail, string confirmationLink);
    }
}
