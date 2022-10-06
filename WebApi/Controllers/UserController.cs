using AutoMapper;
using DTO.Incoming;
using DTO.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Enums;
using SharedServices.Interfaces;
using WebApp.Attributes;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("getCurrent")]
        [Authorize]
        [PermissionAtribute((int)PermissionTypes.RoleAdmin)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await _service.GetCurrentUser();
            var user = _mapper.Map<UserDto>(result);
            return user;
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize]
        [PermissionAtribute((int)PermissionTypes.CanViewAllUsers)]
        [MemoryAttribute(MemoryAttributeConstants.GetAllUsers)]
        // [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<List<UserDto>> GetAll()
        {
            var result = await _service.GetAll();
            var users = result.Select(x => _mapper.Map<UserDto>(x)).ToList();
            return users;
        }
        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        [PermissionAtribute((int)PermissionTypes.CanViewAllUsers)]
        [MemoryAttribute(MemoryAttributeConstants.GetUserById, true)]
        // [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<UserDto>> GetByIdAsync(long id)
        {
            var result = await _service.GetById(id);
            var user = _mapper.Map<UserDto>(result);
            return user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Insert([FromBody] UserInsertDto user)
        {
            var result = await _service.Insert(user);
            return Ok(result);
        }
        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Update([FromBody] UserDto user)
        {
            var result1 = await _service.GetById(user.Id);
            var result = await _service.Update(user);
            return Ok(result);
        }
    }
}
