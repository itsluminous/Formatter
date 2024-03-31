using Microsoft.AspNetCore.Mvc;

namespace ListenerService
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageHandler _messageHandler;

        public MessageController(MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string message)
        {
            _messageHandler.SendMessageAsync(message);
            return Ok();
        }
    }
}