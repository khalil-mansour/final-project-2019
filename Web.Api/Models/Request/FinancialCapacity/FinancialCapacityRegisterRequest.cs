using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FinancialCapacityRegisterRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("annual_income")]
        public int Annual_Income { get; set; }

        [JsonProperty("down_payment")]
        public int Down_Payment { get; set; }

        [JsonProperty("mensual_debt")]
        public int Mensual_Debt { get; set; }

        [JsonProperty("interest_rate")]
        public float Interest_Rate { get; set; }

        [JsonProperty("municipal_taxes")]
        public int Municipal_Taxes { get; set; }

        [JsonProperty("heating_cost")]
        public int Heating_Cost { get; set; }

        [JsonProperty("condo_fee")]
        public int Condo_Fee { get; set; }
    }
}
