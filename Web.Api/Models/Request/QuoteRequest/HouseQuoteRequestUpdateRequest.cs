using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.Api.Models.Request.QuoteRequest
{
    public class HouseQuoteRequestUpdateRequest
    {
        [JsonProperty("uid")]
        public string UserId { get; set; }

        [JsonProperty("house_type_id")]
        public int HouseType { get; set; }

        [JsonProperty("house_location")]
        public HouseLocationCreateRequest Location { get; set; }

        [JsonProperty("listing")]
        public long ListingPrice { get; set; }

        [JsonProperty("down_payment")]
        public long DownPayment { get; set; }

        [JsonProperty("offer")]
        public long Offer { get; set; }

        [JsonProperty("first_house")]
        public bool FirstHouse { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("documents_id")]
        public List<int> DocumentsId { get; set; }

        [JsonProperty("municipal_evaluation")]
        public string MunicipalEvaluationUrl { get; set; }
    }
}
