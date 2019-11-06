
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

        [JsonProperty("city_id")]
        public int CityId { get; set; }

        [JsonProperty("province")]
        public int ProvinceId { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

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
                CityId = houseLocation.CityId,
                ProvinceId = houseLocation.ProvinceId,
                Street = houseLocation.Street,
                AppartementUnits = houseLocation.AppartementUnits
            };
        }
    }
}
