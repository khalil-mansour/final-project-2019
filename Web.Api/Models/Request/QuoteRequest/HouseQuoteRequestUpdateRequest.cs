using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.Api.Models.Request.QuoteRequest
{
    public class HouseQuoteRequestUpdateRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("house_type_id")]
        public int House_Type_Id { get; set; }

        [JsonProperty("house_location")]
        public HouseLocationCreateRequest House_Location { get; set; }

        [JsonProperty("listing_price")]
        public long Listing_Price { get; set; }

        [JsonProperty("down_payment")]
        public long Down_Payment { get; set; }

        [JsonProperty("offer")]
        public long Offer { get; set; }

        [JsonProperty("first_house")]
        public bool First_House { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("documents_id")]
        public List<int> Documents_Id { get; set; }

        [JsonProperty("municipal_evaluation_url")]
        public string Municipal_Evaluation_Url { get; set; }
    }
}
