using ChatApi.Models;

namespace ChatApi.Hub
{
    public interface IChatHub
    {
        Task BroadcastMessage(Message message);

    }
}
