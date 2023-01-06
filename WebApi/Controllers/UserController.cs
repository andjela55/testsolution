using AutoMapper;
using DTO.Incoming;
using DTO.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
        //[MemoryAttribute(MemoryAttributeConstants.GetAllUsers)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        //[ETagFilter(200)]
        public async Task<List<UserDto>> GetAll()
        {
            var result = await _service.GetAll();
            var users = result.Select(x => _mapper.Map<UserDto>(x)).ToList();
            return users;
        }
        [HttpGet]
        [Route("get/{id}")]
        [PermissionAtribute((int)PermissionTypes.CanViewAllUsers)]
        //[MemoryAttribute(MemoryAttributeConstants.GetUserById, true)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
        //[OutputCache(PolicyName = "Expire20")]
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
        public async Task<ActionResult<bool>> Update([FromBody] UserInsertDto user)
        {
            var result = await _service.Update(user);
            return Ok(result);
        }
        [HttpGet]
        [Route("getTime")]
        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Client, NoStore = false)]
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.None, NoStore = false)]
        //[ETagFilter(200)]
        [OutputCache(PolicyName = "Expire20")]
        public async Task<ActionResult<DateTime>> GetTime()
        {
            var result = DateTime.UtcNow;
            return Ok(result);
        }
        [HttpGet]
        [Route("get/file")]
        //[MemoryAttribute(MemoryAttributeConstants.GetUserById, true)]
        [ResponseCache(Duration = 50, Location = ResponseCacheLocation.Client, NoStore = false)]
        //[OutputCache(PolicyName = "Expire20")]
        public async Task<ActionResult<string>> GetData()
        {
            //char[] chars = new char[162278964];
            char[] chars = new char[60000000];
            // Optional step - unnecessary if you're happy with the array being full of \0
            string str = new string(chars);

            return Ok(str);
        }
    }
}
