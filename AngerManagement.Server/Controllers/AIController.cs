using Microsoft.AspNetCore.Mvc;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace AngerManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private ChatClient client = new ChatClient(
            model: "deepseek-chat",
            credential: new ApiKeyCredential(Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY")),
            options: new OpenAIClientOptions()
            {
                Endpoint = new Uri(Environment.GetEnvironmentVariable("DEEPSEEK_BASE_URI"))
            }
        );

        [HttpGet("completechat")]
        public async Task<IActionResult> RequestAnswer([FromBody] string message)
        {
            ChatCompletion result = await client.CompleteChatAsync(message);
            return Ok( new { response = result.Content[0].Text } );
        }

        [HttpGet("streamchat")]
        public async Task<IActionResult> RequestStreamedAnswer([FromBody] string message)
        {
            ChatCompletion result = await client.CompleteChatAsync(message);
            return Ok(new { response = result.Content[0].Text });
        }
    }
}
