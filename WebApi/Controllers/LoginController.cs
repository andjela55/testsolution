using DTO.Incoming;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginUser(LoginDto loginData)
        {
            var token = await _service.LoginUser(loginData);
            return Ok(token);
        }

        [HttpPost]
        [Route("setPassword")]
        public async Task<ActionResult<string>> SetInitialPassword(LoginDto loginData)
        {
            var token = await _service.SetInitialPassword(loginData);
            return Ok(token);
        }

    }
}
