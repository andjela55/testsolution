using AutoMapper;
using DTO.Incoming.MessageDtoClass;
using DTO.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.Models.ChatHubClass;
using SharedServices.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private ChatConnectionMapping _connectionDictonary;
        private IUserService _userService;
        private readonly IMapper _mapper;

        public ChatController(IHubContext<ChatHub> hubContext,
                              ChatConnectionMapping connectionDictonary,
                              IUserService userService,
                              IMapper mapper)
        {
            _hubContext = hubContext;
            _connectionDictonary = connectionDictonary;
            _userService = userService;
            _mapper = mapper;
        }
        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] MessageDto msg)
        {
            var currentUser = await _userService.GetCurrentUser();
            _hubContext.Clients.All.SendAsync("ReceiveMessage", currentUser.Name, msg.Message);
            return Ok();
        }
        [Route("sendToReceiver")]
        [HttpPost]
        public async Task<IActionResult> SendRequestToReceiver([FromBody] MessageDto data)
        {
            var currentUser = await _userService.GetCurrentUser();
            var connectionIds = _connectionDictonary.GetConnections(data.Receiver).ToList();
            for (int i = 0; i < connectionIds.Count(); i++)
            {
                _hubContext.Clients.Client(connectionIds[i]).SendAsync("ReceiveMessage", currentUser.Name, data.Message);

            }
            return Ok();
        }
        [Route("activeUsers")]
        [HttpGet]
        public async Task<List<UserDto>> GetActiveUsers()
        {
            var usersIds = _connectionDictonary.GetConnectedUsersIds();
            var ids = new List<long>();
            usersIds.ForEach(x =>
            {
                var y = (long)Int32.Parse(x);
                ids.Add(y);
            });

            var result = await _userService.GetByIds(ids);
            var users = result.Select(x => _mapper.Map<UserDto>(x)).ToList();
            return users;
        }
    }
}
