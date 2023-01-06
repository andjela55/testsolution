using DTO.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IRegisterService _service;
        public RegisterController(IRegisterService service)
        {
            _service = service;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> RegisterUser([FromBody] UserInsertDto userData)
        {
            await _service.RegisterUser(userData);
            return Ok();
        }

        [HttpPost]
        [Route("confirm/{id}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ConfirmRegistration(long id, string token)
        {
            await _service.ConfirmRegistration(id, token);
            return Ok();
        }
    }
}
