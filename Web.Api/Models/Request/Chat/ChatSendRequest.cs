using Newtonsoft.Json;


namespace Web.Api.Models.Request.Chat
{
    public class ChatSendRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("quote_id")]
        public int Quote_Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
