using Newtonsoft.Json;


namespace Web.Api.Models.Request.Chat
{
    public class ChatFetchRequest
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
