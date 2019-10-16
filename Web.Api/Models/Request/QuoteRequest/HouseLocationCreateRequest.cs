using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class HouseLocationCreateRequest
    {

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("city_id")]
        public int CityId  {get; set;}

        [JsonProperty("province")]
        public int ProvinceId { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("appartement_units")]
        public int AppartementUnits { get; set; }
    }
}
