using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Models;

namespace ChatHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHubBase> _hubContext;
        public ChatController(IHubContext<ChatHubBase> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }
    }
}
