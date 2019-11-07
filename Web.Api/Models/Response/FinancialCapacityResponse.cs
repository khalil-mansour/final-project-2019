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
