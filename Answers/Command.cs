using chatBot.Utility;
using Newtonsoft.Json;

namespace chatBot.Answers
{
    public class Command
    {
        public string Name { get; set; }
        [JsonConverter(typeof(HashSetConverter))]
        public HashSet<string> Tags { get; set; }
        public string Answer { get; set; }
    }
}
