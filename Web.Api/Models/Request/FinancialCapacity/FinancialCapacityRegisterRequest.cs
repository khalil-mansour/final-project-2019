using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FinancialCapacityRegisterRequest
    {
        [JsonProperty("uid")]
        public string Id { get; set; }

        [JsonProperty("annualIncome")]
        public int AnnualIncome { get; set; }

        [JsonProperty("downPayment")]
        public int DownPayment { get; set; }

        [JsonProperty("mensualDebt")]
        public int MensualDebt { get; set; }

        [JsonProperty("interestRate")]
        public float InterestRate { get; set; }

        [JsonProperty("municipalTaxes")]
        public int MunicipalTaxes { get; set; }

        [JsonProperty("heatingCost")]
        public int HeatingCost { get; set; }

        [JsonProperty("condoFee")]
        public int CondoFee { get; set; }
    }
}
