using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class ChatResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("quote_id")]
        public int QuoteId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        public static string ToJson(Chat chat)
        {
            var response = new ChatResponse
            {
                Id = chat.Id,
                UserId = chat.UserId,
                QuoteId = chat.QuoteId,
                Message = chat.Message,
                Timestamp = chat.TimeStamp.ToString()
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
