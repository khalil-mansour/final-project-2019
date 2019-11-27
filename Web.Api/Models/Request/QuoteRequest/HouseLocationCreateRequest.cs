using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request.QuoteRequest
{
    public class HouseLocationCreateRequest
    {

        [JsonProperty("postal_code")]
        public string Postal_Code { get; set; }

        [JsonProperty("city")]
        public string City  {get; set;}

        [JsonProperty("province_id")]
        public int Province_Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("apartment_unit")]
        public string Apartment_Unit { get; set; }
    }
}
