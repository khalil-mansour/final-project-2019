using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class HouseQuoteRequestResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("house_type_id")]
        public int HouseType { get; set; }

        [JsonProperty("house_location")]
        public HouseLocationResponse HouseLocation { get; set; }

        [JsonProperty("listing")]
        public long ListingPrice { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("down_payment")]
        public long DownPayment { get; set; }

        [JsonProperty("offer")]
        public long Offer { get; set; }

        [JsonProperty("first_house")]
        public bool FirstHouse { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("municipal_evaluation")]
        public string MunicipalEvaluationUrl { get; set; }

        public HouseQuoteRequestResponse()
        {
        }

        public static string ToJson(HouseQuoteRequest houseQuoteRequest)
        {
            var response = new HouseQuoteRequestResponse()
            {
                Id = houseQuoteRequest.Id,
                HouseType = houseQuoteRequest.HouseType,
                HouseLocation = HouseLocationResponse.MapProperty(houseQuoteRequest.HouseLocation),
                ListingPrice = houseQuoteRequest.ListingPrice,
                CreatedDate = houseQuoteRequest.CreatedDate,
                DownPayment = houseQuoteRequest.DownPayment,
                Offer = houseQuoteRequest.Offer,
                FirstHouse = houseQuoteRequest.FirstHouse,
                Description = houseQuoteRequest.Description,
                MunicipalEvaluationUrl = houseQuoteRequest.MunicipalEvaluationUrl
            };
            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }
        public static string ToJson(IEnumerable<HouseQuoteRequest> houseQuoteRequests)
        {
            List<HouseQuoteRequestResponse> responses = new List<HouseQuoteRequestResponse>();
            houseQuoteRequests.ToList().ForEach(x => responses.Add(
                new HouseQuoteRequestResponse
                {
                    Id = x.Id,
                    HouseType = x.HouseType,
                    HouseLocation = HouseLocationResponse.MapProperty(x.HouseLocation),
                    ListingPrice = x.ListingPrice,
                    DownPayment = x.DownPayment,
                    Offer = x.Offer,
                    FirstHouse = x.FirstHouse,
                    Description = x.Description,
                    MunicipalEvaluationUrl = x.MunicipalEvaluationUrl

                }
                ));
            return JsonConvert.SerializeObject(responses, Formatting.Indented);
        }
    }
}
