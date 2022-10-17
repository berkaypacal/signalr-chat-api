using ChatApi.Hub;
using ChatApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.BroadcastMessage(message);
        }

        public override async Task OnConnectedAsync()
        {

            Message message = new Message();
            message.connectionId = Context.ConnectionId;
            message.message = "Kullanıcı özel mesaj";
            await Clients.Client(Context.ConnectionId).BroadcastMessage(message);
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Message message = new Message();
            message.message = "Disconnected: "+Context.ConnectionId;
            Console.WriteLine(message);
            await Clients.All.BroadcastMessage(message);
            await base.OnDisconnectedAsync(exception);
        }



        public string UserConnected(string userId)
        {
            string message = "User Id: " + userId + " Connection Id: " + Context.ConnectionId;
            return message;
        }
    }
}
