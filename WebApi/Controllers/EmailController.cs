using Microsoft.AspNetCore.Mvc;
using SharedServices.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private IEmailHelperService _service;
        public EmailController(IEmailHelperService service)
        {
            _service = service;
        }

        //public async Task<bool> ConfirmEmail(string token, string email)
        //{
        //    //_service.ConfirmEmail();
        //    //var user = await userManager.FindByEmailAsync(email);
        //    //if (user == null)
        //    //    return View("Error");

        //    //var result = await userManager.ConfirmEmailAsync(user, token);
        //    //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}
    }
}
