using Microsoft.Extensions.Configuration;
using SharedRepository;
using SharedServices.Interfaces;
using System.Net.Mail;

namespace Services.HelperServices
{
    public class EmailHelperService : IEmailHelperService
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;

        public EmailHelperService(IConfiguration config,
                            IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }
        public async Task<bool> ConfirmEmail(long id, string token)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User does not exist");
            }
            if (user.AccountConfirmed)
            {
                throw new UnauthorizedAccessException("Email is already validated");
            }

            return true;
        }
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("care@yogihosting.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("", "");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
