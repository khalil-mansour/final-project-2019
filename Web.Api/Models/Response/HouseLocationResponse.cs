
using Newtonsoft.Json;
using System;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class HouseLocationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public int City { get; set; }

        [JsonProperty("province")]
        public int Province { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("appartement_units")]
        public int AppartementUnits { get; set; }

        public HouseLocationResponse()
        {

        }

        public static HouseLocationResponse MapProperty(HouseLocation houseLocation)
        {
            return new HouseLocationResponse()
            {
                Id = houseLocation.Id,
                PostalCode = houseLocation.PostalCode,
                City = houseLocation.City,
                Province = houseLocation.Province,
                address = houseLocation.address,
                AppartementUnits = houseLocation.AppartementUnits
            };
        }
    }
}
