using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class OfferResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("quote_request_id")]
        public int QuoteRequestId { get; set; }

        [JsonProperty("annual_interest_rate")]
        public double AnnualInterestRate { get; set; }

        [JsonProperty("loan")]
        public double Loan { get; set; }

        [JsonProperty("mensuality")]
        public double Mensuality { get; set; }

        [JsonProperty("rate_type")]
        public int RateType { get; set; }

        [JsonProperty("contract_duration")]
        public int ContractDuration { get; set; }

        [JsonProperty("loan_duration")]
        public int LoanDuration { get; set; }

        [JsonProperty("payment_frequency")]
        public int PaymentFrequency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submitted")]
        public bool Submitted { get; set; }

        public static string ToJson(Offer offer)
        {
            var response = new OfferResponse
            {
                Id = offer.Id,
                UserId = offer.UserId,
                QuoteRequestId = offer.QuoteRequestId,
                AnnualInterestRate = offer.AnnualInterestRate,
                Loan = offer.Loan,
                Mensuality = offer.Mensuality,
                RateType = offer.RateType,
                ContractDuration = offer.ContractDuration,
                LoanDuration = offer.LoanDuration,
                PaymentFrequency = offer.PaymentFrequency,
                Description = offer.Description,
                Submitted = offer.Submitted
            };

            return JsonConvert.SerializeObject(response);
        }

        public static string ToJson(IEnumerable<Offer> offers)
        {
            List<OfferResponse> responses = new List<OfferResponse>();
            foreach (var offer in offers)
            {
                var response = new OfferResponse
                {
                    Id = offer.Id,
                    UserId = offer.UserId,
                    QuoteRequestId = offer.QuoteRequestId,
                    AnnualInterestRate = offer.AnnualInterestRate,
                    Loan = offer.Loan,
                    Mensuality = offer.Mensuality,
                    RateType = offer.RateType,
                    ContractDuration = offer.ContractDuration,
                    LoanDuration = offer.LoanDuration,
                    PaymentFrequency = offer.PaymentFrequency,
                    Description = offer.Description,
                    Submitted = offer.Submitted
                };
                responses.Add(response);
            }
            return JsonConvert.SerializeObject(responses);
        }
    }
}
