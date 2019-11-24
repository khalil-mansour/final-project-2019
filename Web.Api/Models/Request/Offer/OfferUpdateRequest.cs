using Newtonsoft.Json;

namespace Web.Api.Models.Request.Offer
{
    public class OfferUpdateRequest
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("quoterequestid")]
        public int QuoteRequestId { get; set; }

        [JsonProperty("annualinterestrate")]
        public double AnnualInterestRate { get; set; }

        [JsonProperty("loan")]
        public double Loan { get; set; }

        [JsonProperty("mensuality")]
        public double Mensuality { get; set; }

        [JsonProperty("ratetype")]
        public int RateType { get; set; }

        [JsonProperty("contractduration")]
        public int ContractDuration { get; set; }

        [JsonProperty("loanduration")]
        public int LoanDuration { get; set; }

        [JsonProperty("paymentfrequency")]
        public int PaymentFrequency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submitted")]
        public bool Submitted { get; set; }
    }
}