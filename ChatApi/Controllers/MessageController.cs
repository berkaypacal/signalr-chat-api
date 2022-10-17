using ChatApi.Hub;
using ChatApi.Hubs;
using ChatApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private IHubContext<ChatHub, IChatHub> _hubContext;

        public MessageController(IHubContext<ChatHub, IChatHub> hubContext)
        {
            _hubContext = hubContext;
        }


     

        // POST api/<MessageController>
        [HttpPost]
        public string Post([FromBody] Message message)
        {

            string retMessage = string.Empty;
            try
            {
                if(message.connectionId != "" && message.connectionId != string.Empty)
                {
                    _hubContext.Clients.Client(message.connectionId).BroadcastMessage(message);
                    retMessage = "Success "+message.connectionId;
                }
                else
                {
                    _hubContext.Clients.All.BroadcastMessage(message);
                    retMessage = "Success";
                }

                
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return retMessage;


        }



    }
}
