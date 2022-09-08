using SharedRepository;
using SharedServices.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Services.HelperServices
{
    public class EmailHelperService : IEmailHelperService
    {
        private IUserRepository _userRepository;

        public EmailHelperService(
                            IUserRepository userRepository)
        {
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
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("testandjela55@gmail.com");
                    mail.To.Add("andjelafilipovic1416@gmail.com");
                    mail.Subject = "Test Mail";
                    mail.Body = $"<h1>Please confirm you registration</h1>\n<a href=\"{confirmationLink}\">click here!</a>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("testandjela55@gmail.com", "tuvbeqwtzjatvojd");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
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
