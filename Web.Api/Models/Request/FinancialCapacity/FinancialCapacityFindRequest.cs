using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FinancialCapacityFindRequest
    {
        [JsonProperty("user_id")]
        public string Id { get; set; }
    }
}