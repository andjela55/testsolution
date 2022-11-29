using AutoMapper;
using DTO.Incoming;
using DTO.Outgoing.LoginDtoClass;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces.Models;
using SharedServices.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _service;
        private readonly IMapper _mapper;
        public LoginController(ILoginService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ILoginResponse>> LoginUser([FromBody] LoginDto loginData)
        {
            var result = await _service.LoginUser(loginData);
            var response = _mapper.Map<LoginResponseDto>(result);
            return Ok(response);
        }

        [HttpPost]
        [Route("setPassword")]
        public async Task<ActionResult<string>> SetInitialPassword(LoginDto loginData)
        {
            var token = await _service.SetInitialPassword(loginData);
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<ILoginResponse>> RefreshTokens([FromBody] string refreshToken)
        {
            var result = await _service.RefreshTokens(refreshToken);
            var response = _mapper.Map<LoginResponseDto>(result);
            return Ok(response);
        }

    }
}
