using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class FinancialCapacityResponse
    {
        [JsonProperty("user_id")]
        public string Id { get; set; }

        [JsonProperty("annual_income")]
        public int AnnualIncome { get; set; }

        [JsonProperty("down_payment")]
        public int DownPayment { get; set; }

        [JsonProperty("mensual_debt")]
        public int MensualDebt { get; set; }

        [JsonProperty("interest_rate")]
        public float InterestRate { get; set; }

        [JsonProperty("municipal_taxes")]
        public int MunicipalTaxes { get; set; }

        [JsonProperty("heating_cost")]
        public int HeatingCost { get; set; }

        [JsonProperty("condo_fee")]
        public int CondoFee{ get; set; }

        public FinancialCapacityResponse() {
        }

       public static string ToJson(FinancialCapacity financialCapacity) {
            var response = new FinancialCapacityResponse()
            {
                Id = financialCapacity.Id,
                AnnualIncome = financialCapacity.AnnualIncome,
                DownPayment = financialCapacity.DownPayment,
                MensualDebt = financialCapacity.MensualDebt,
                InterestRate = financialCapacity.InterestRate,
                MunicipalTaxes = financialCapacity.MunicipalTaxes,
                HeatingCost = financialCapacity.HeatingCost,
                CondoFee = financialCapacity.CondoFee

            };

            return JsonConvert.SerializeObject(response, Formatting.Indented);

        }
    }
}
