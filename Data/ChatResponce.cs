using chatBot.Models;

namespace chatBot.Data
{
    public class ChatResponse
    {
        public PostModel UserMessage { get; set; }
        public PostModel BotResponse { get; set; }
    }
}
