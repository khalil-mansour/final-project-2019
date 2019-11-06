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

        [JsonProperty("city")]
        public string City  {get; set;}

        [JsonProperty("province_id")]
        public int ProvinceId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("appartement_units")]
        public int AppartementUnits { get; set; }
    }
}
