using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Offer;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class OfferRepository : IOfferRepository
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;

        public OfferRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public Offer GetOffer(int id)
        {
            var select_query = $@"SELECT
                                  id as { nameof(Offer.Id) },
                                  user_id as { nameof(Offer.UserId) },
                                  request_id as { nameof(Offer.QuoteRequestId) },
                                  annual_interest_rate as { nameof(Offer.AnnualInterestRate) },
                                  loan as { nameof(Offer.Loan) },
                                  mensuality as { nameof(Offer.Mensuality) },
                                  rate_type as { nameof(Offer.RateType) },
                                  contract_duration as { nameof(Offer.ContractDuration) },
                                  loan_duration as { nameof(Offer.LoanDuration) },
                                  payment_frequency as { nameof(Offer.PaymentFrequency) },
                                  description as { nameof(Offer.Description) },
                                  submitted as { nameof(Offer.Submitted) }
                                  FROM public.quote
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    return conn.Query<Offer>(select_query, new { id }).FirstOrDefault();
                }
                catch (NpgsqlException e)
                {
                    throw (e);
                }
            }
        }

        public async Task<OfferCreateRepoResponse> Create(Offer offer)
        {
            var add_query = $@"INSERT INTO public.quote (user_id, request_id, submitted)
                               VALUES (@userid, @quoterequestid, @submitted)
                               RETURNING id;";

            var select_query = $@"SELECT
                                  id as { nameof(Offer.Id) },
                                  user_id as { nameof(Offer.UserId) },
                                  request_id as { nameof(Offer.QuoteRequestId) },
                                  submitted as { nameof(Offer.Submitted) }
                                  FROM public.quote
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // add offer
                    var offerID = conn.Query<int>(add_query, offer).Single();
                    var response = conn.Query<Offer>(select_query, new { id = offerID }).FirstOrDefault();

                    // commit
                    transaction.Commit();

                    // return the response
                    return new OfferCreateRepoResponse(response, true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new OfferCreateRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<OfferFetchRepoResponse> Fetch(int offerId)
        {
            var select_query = $@"SELECT
                                  id as { nameof(Offer.Id) },
                                  user_id as { nameof(Offer.UserId) },
                                  request_id as { nameof(Offer.QuoteRequestId) },
                                  annual_interest_rate as { nameof(Offer.AnnualInterestRate) },
                                  loan as { nameof(Offer.Loan) },
                                  mensuality as { nameof(Offer.Mensuality) },
                                  rate_type as { nameof(Offer.RateType) },
                                  contract_duration as { nameof(Offer.ContractDuration) },
                                  loan_duration as { nameof(Offer.LoanDuration) },
                                  payment_frequency as { nameof(Offer.PaymentFrequency) },
                                  description as { nameof(Offer.Description) },
                                  submitted as { nameof(Offer.Submitted) }
                                  FROM public.quote
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // return the response
                    return new OfferFetchRepoResponse(GetOffer(offerId), true);
                }
                catch (Exception e)
                {
                    // return the response
                    return new OfferFetchRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<OfferDeleteRepoResponse> Delete(int offerId)
        {
            var delete_offer_query = $@"DELETE FROM public.quote WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // fetch document
                    var response = GetOffer(offerId);
                    // delete document
                    var success = Convert.ToBoolean(conn.Execute(delete_offer_query, new { id = offerId }));
                    // commit
                    transaction.Commit();
                    // return the response
                    return new OfferDeleteRepoResponse(response, success);
                }
                catch (Exception e)
                {
                    // return the response
                    return new OfferDeleteRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<OfferFetchAllRepoResponse> FetchAllByUser(string userId)
        {
            var select_all_query = $@"SELECT
                                  id as { nameof(Offer.Id) },
                                  user_id as { nameof(Offer.UserId) },
                                  request_id as { nameof(Offer.QuoteRequestId) },
                                  annual_interest_rate as { nameof(Offer.AnnualInterestRate) },
                                  loan as { nameof(Offer.Loan) },
                                  mensuality as { nameof(Offer.Mensuality) },
                                  rate_type as { nameof(Offer.RateType) },
                                  contract_duration as { nameof(Offer.ContractDuration) },
                                  loan_duration as { nameof(Offer.LoanDuration) },
                                  payment_frequency as { nameof(Offer.PaymentFrequency) },
                                  description as { nameof(Offer.Description) },
                                  submitted as { nameof(Offer.Submitted) }
                                  FROM public.quote
                                  WHERE user_id = @userid";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // return the response
                    return new OfferFetchAllRepoResponse(conn.Query<Offer>(select_all_query, new { userId }).ToList(), true);
                }
                catch (Exception e)
                {
                    // return the response
                    return new OfferFetchAllRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<OfferFetchAllByReqRepoResponse> FetchAllByRequest(int requestId)
        {
            var select_all_query = $@"SELECT
                                  id as { nameof(Offer.Id) },
                                  user_id as { nameof(Offer.UserId) },
                                  request_id as { nameof(Offer.QuoteRequestId) },
                                  annual_interest_rate as { nameof(Offer.AnnualInterestRate) },
                                  loan as { nameof(Offer.Loan) },
                                  mensuality as { nameof(Offer.Mensuality) },
                                  rate_type as { nameof(Offer.RateType) },
                                  contract_duration as { nameof(Offer.ContractDuration) },
                                  loan_duration as { nameof(Offer.LoanDuration) },
                                  payment_frequency as { nameof(Offer.PaymentFrequency) },
                                  description as { nameof(Offer.Description) },
                                  submitted as { nameof(Offer.Submitted) }
                                  FROM public.quote
                                  WHERE request_id = @requestId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // return the response
                    return new OfferFetchAllByReqRepoResponse(conn.Query<Offer>(select_all_query, new { requestId }).ToList(), true);
                }
                catch (Exception e)
                {
                    // return the response
                    return new OfferFetchAllByReqRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<OfferUpdateRepoResponse> Update(int offerId, Offer offer)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var update_offer_query = $@"UPDATE public.quote
                                                SET annual_interest_rate = @annualInterestRate,
                                                    loan = @loan,
                                                    mensuality = @mensuality,
                                                    rate_type = @rateType,
                                                    contract_duration = @contractDuration,
                                                    loan_duration = @loanDuration,
                                                    payment_frequency = @paymentFrequency,
                                                    description = @description,
                                                    submitted = @submitted
                                                WHERE id = @id";

                conn.Open();
                var transaction = conn.BeginTransaction();

                try
                {
                    conn.Execute(update_offer_query,
                        new
                        {
                            id = offerId,
                            annualInterestRate = offer.AnnualInterestRate,
                            loan = offer.Loan,
                            mensuality = offer.Mensuality,
                            rateType = offer.RateType,
                            contractDuration = offer.ContractDuration,
                            loanDuration = offer.LoanDuration,
                            paymentFrequency = offer.PaymentFrequency,
                            description = offer.Description,
                            submitted = offer.Submitted
                        });

                    // commit
                    transaction.Commit();
                    // return the response
                    return new OfferUpdateRepoResponse(GetOffer(offerId), true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new OfferUpdateRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }
    }
}
