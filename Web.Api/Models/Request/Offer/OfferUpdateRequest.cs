using Newtonsoft.Json;

namespace Web.Api.Models.Request.Offer
{
    public class OfferUpdateRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("annual_interest_rate")]
        public double Annual_Interest_Rate { get; set; }

        [JsonProperty("loan")]
        public double Loan { get; set; }

        [JsonProperty("mensuality")]
        public double Mensuality { get; set; }

        [JsonProperty("rate_type")]
        public int Rate_Type { get; set; }

        [JsonProperty("contract_duration")]
        public int Contract_Duration { get; set; }

        [JsonProperty("loan_duration")]
        public int Loan_Duration { get; set; }

        [JsonProperty("payment_frequency")]
        public int Payment_Frequency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submitted")]
        public bool Submitted { get; set; }
    }
}