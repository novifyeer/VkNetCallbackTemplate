using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Model;

namespace VkBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CallbackController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Тип события
            switch (updates.Type)
            {
                // Ключ-подтверждение
                case "confirmation":

                    return new OkObjectResult(_configuration["Config:Confirmation"]);

                // Новое сообщение
                case "message_new":
                    // Десериализация
                    var msg = JsonConvert.DeserializeObject<Message>(updates.Object.ToString());
                    
                    break;
            }

            return new OkObjectResult("ok");
        }
    }
}