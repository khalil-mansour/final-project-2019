using Newtonsoft.Json;

namespace Web.Api.Models.Request.Offer
{
    public class OfferCreateRequest
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("quoterequestid")]
        public int QuoteRequestId { get; set; }

        [JsonProperty("submitted")]
        public bool Submitted { get; set; }
    }
}
