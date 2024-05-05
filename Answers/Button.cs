using chatBot.Utility;
using Newtonsoft.Json;

namespace chatBot.Answers
{
    public class Button
    {
        public string name;
        [JsonConverter(typeof(HashSetConverter))]
        public HashSet<string> tags { get; set; } = new HashSet<string>();
        public string URL;

    }
}
