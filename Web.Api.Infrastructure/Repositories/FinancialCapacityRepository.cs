using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Linq;
using System;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Dto;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class FinancialCapacityRepository : IFinancialCapacityRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        public FinancialCapacityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public async Task<FinancialCapacityRegisterRepoResponse> Create(FinancialCapacity financialCapacity)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var add_query = $@"INSERT INTO public.financial_capacity (uid, annualIncome, downPayment, mensualDebt, interestRate, municipalTaxes, heatingCost, condoFee)
                               VALUES (@id, @annualIncome, @downPayment, @mensualDebt, @interestRate, @municipalTaxes, @heatingCost, @condoFee)
                               ON CONFLICT ON CONSTRAINT financial_capacity_pkey DO UPDATE SET annualIncome = @annualIncome, downPayment = @downPayment, mensualDebt = @mensualDebt, 
                                interestRate = @interestRate, municipalTaxes = @municipalTaxes, heatingCost = @heatingCost, condoFee = @condoFee;";


            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // add financialCapacity
                    var success = Convert.ToBoolean(conn.Execute(add_query, financialCapacity));

                    // return the response
                    return new FinancialCapacityRegisterRepoResponse(FindFinancialCapacityById(financialCapacity.Id), success);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FinancialCapacityRegisterRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        private FinancialCapacity FindFinancialCapacityById(string uid)
        {

            var select_query = $@"SELECT uid AS {nameof(FinancialCapacity.Id)},
                                    annualIncome AS {nameof(FinancialCapacity.AnnualIncome)},
                                    downPayment AS {nameof(FinancialCapacity.DownPayment)},
                                    mensualDebt AS {nameof(FinancialCapacity.MensualDebt)},
                                    interestRate AS {nameof(FinancialCapacity.InterestRate)},
                                    municipalTaxes AS {nameof(FinancialCapacity.MunicipalTaxes)},
                                    heatingCost AS {nameof(FinancialCapacity.HeatingCost)},
                                    condoFee AS {nameof(FinancialCapacity.CondoFee)}
                                  FROM public.financial_capacity WHERE uid=@uid";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = conn.Query<FinancialCapacity>(select_query, new { uid }).FirstOrDefault();
                return query;
            }
        }


        public async Task<FinancialCapacityFindRepoResponse> FindById(string id)
        {
            try
            {
                var returnedUser = new FinancialCapacityFindRepoResponse(FindFinancialCapacityById(id), true);
                if (returnedUser.FinancialCapacity == null)
                {
                    return new FinancialCapacityFindRepoResponse(null, false, new[] { new Error("auth/financial-capacity-not-found", "user not found") });
                }

                return returnedUser;
            }
            catch (NpgsqlException e)
            {
                // return the response
                return new FinancialCapacityFindRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
            }
        }
    }
}
