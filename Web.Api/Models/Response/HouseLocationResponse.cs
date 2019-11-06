
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
        public string City { get; set; }

        [JsonProperty("province_id")]
        public int ProvinceId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

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
                ProvinceId = houseLocation.ProvinceId,
                Address = houseLocation.Address,
                AppartementUnits = houseLocation.AppartementUnits
            };
        }
    }
}
