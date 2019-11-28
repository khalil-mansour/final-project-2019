using Newtonsoft.Json;


namespace Web.Api.Models.Request.Chat
{
    public class ChatFetchRequest
    {
        [JsonProperty("quote_id")]
        public string Quote_Id { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
