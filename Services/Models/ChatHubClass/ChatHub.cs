using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Services.Models.ChatHubClass
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        private ChatConnectionMapping _connectionDictionary;
        public ChatHub(ChatConnectionMapping connectionDictionary)
        {
            _connectionDictionary = connectionDictionary;
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public Task SendMessageToReceiver(string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", message);
        }
        public override Task OnConnectedAsync()
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var id = "";
            if (identity != null)
            {
                id = identity!.FindFirst("Id").Value;

            }

            _connectionDictionary.Add(id, Context.ConnectionId);

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var id = "";
            if (identity != null)
            {
                id = identity!.FindFirst("Id").Value;

            }

            _connectionDictionary.Remove(id, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

    }
}
