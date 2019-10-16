using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseLocation
    {


        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; }

        [JsonProperty("city_id")]
        public int CityId { get; }

        [JsonProperty("province")]
        public int ProvinceId { get; }

        [JsonProperty("street")]
        public string Street { get; }

        [JsonProperty("appartement_units")]
        public int AppartementUnits { get; }

        public HouseLocation(string id, string postalCode, int cityId, int provinceId, string street, int appartementUnits)
        {
            Id = id;
            PostalCode = postalCode;
            CityId = cityId;
            ProvinceId = provinceId;
            Street = street;
            AppartementUnits = appartementUnits;
        }

    }
}
