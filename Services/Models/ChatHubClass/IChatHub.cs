namespace Services.Models.ChatHubClass
{
    public interface IChatHub
    {
        Task SendMessageToClient(string message);
    }
}
