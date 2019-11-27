using Newtonsoft.Json;

namespace Web.Api.Models.Request.Offer
{
    public class OfferCreateRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("quote_request_id")]
        public int Quote_Request_Id { get; set; }

        [JsonProperty("submitted")]
        public bool Submitted { get; set; }
    }
}
