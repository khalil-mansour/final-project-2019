using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class QuoteRequestRepository : IQuoteRequestRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public QuoteRequestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;

        }
        public async Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest)
        {

            var addHouseLocationQuery = $@"INSERT INTO public.house_location (postalcode, city_id, province_id, street, appartement_units)
                               VALUES (@PostalCode, @CityId, @ProvinceId, @Street, @AppartementUnits)
                               RETURNING id;";

            var addHouseQuoteRequest = $@"INSERT INTO public.quote_request_house (user_id, house_type_id, house_location_id, listing, created_date, down_payment, offer, first_house, description, municipal_evaluation)
                               VALUES (@UserId, @HouseType, @HouseLocationId, @ListingPrice, @CreationDate, @DownPayment, @Offer, @FirstHouse, @Description, @MunicipalEvaluationUrl)
                               RETURNING id;";


            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var houseLocationId = conn.Query<int>(addHouseLocationQuery, houseQuoteRequest.HouseLocation).Single();
                    var quoteRequestId = conn.Query<int>(addHouseQuoteRequest,
                        new
                        {
                            houseQuoteRequest.UserId,
                            houseQuoteRequest.HouseType,
                            houseLocationId,
                            houseQuoteRequest.ListingPrice,
                            houseQuoteRequest.CreatedDate,
                            houseQuoteRequest.DownPayment,
                            houseQuoteRequest.Offer,
                            houseQuoteRequest.FirstHouse,
                            houseQuoteRequest.Description,
                            houseQuoteRequest.MunicipalEvaluationUrl
                        }).Single();

                    return new HouseQuoteRequestCreateRepoResponse(FindQuoteRequestById(quoteRequestId), true);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new HouseQuoteRequestCreateRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }
        public async Task<HouseQuoteRequestGetAllRepoResponse> GetAllQuoteForUser(string userId)
        {

            var select_query = $@"SELECT id AS {nameof(HouseQuoteRequest.Id)},
                                    house_type_id AS {nameof(HouseQuoteRequest.HouseType)},
                                    house_location_id AS {nameof(HouseQuoteRequest.HouseLocationId)},
                                    listing AS {nameof(HouseQuoteRequest.ListingPrice)},
                                    created_date AS {nameof(HouseQuoteRequest.CreatedDate)}, 
                                    down_payment AS {nameof(HouseQuoteRequest.DownPayment)},
                                    offer AS {nameof(HouseQuoteRequest.Offer)},
                                    first_house AS {nameof(HouseQuoteRequest.FirstHouse)},
                                    description AS {nameof(HouseQuoteRequest.Description)},
                                    municipal_evaluation AS {nameof(HouseQuoteRequest.MunicipalEvaluationUrl)}
                                  FROM public.quote_request_house WHERE user_id = @userId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var houseQuoteRequests = conn.Query<HouseQuoteRequest>(select_query, new { userId }).ToList();
                    houseQuoteRequests.ForEach(x => x.HouseLocation = FindHouseLocationById(x.HouseLocationId));

                    return new HouseQuoteRequestGetAllRepoResponse(houseQuoteRequests, true);
                }
                catch (NpgsqlException e)
                {
                    return new HouseQuoteRequestGetAllRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        
        }

        private HouseQuoteRequest FindQuoteRequestById(int quoteRequestId)
        {
            var select_query = $@"SELECT id AS {nameof(HouseQuoteRequest.Id)},
                                    house_type_id AS {nameof(HouseQuoteRequest.HouseType)},
                                    house_location_id AS {nameof(HouseQuoteRequest.HouseLocationId)},
                                    listing AS {nameof(HouseQuoteRequest.ListingPrice)},
                                    down_payment AS {nameof(HouseQuoteRequest.DownPayment)},
                                    offer AS {nameof(HouseQuoteRequest.Offer)},
                                    first_house AS {nameof(HouseQuoteRequest.FirstHouse)},
                                    description AS {nameof(HouseQuoteRequest.Description)},
                                    municipal_evaluation AS {nameof(HouseQuoteRequest.MunicipalEvaluationUrl)}
                                  FROM public.quote_request_house WHERE id=@quoteRequestId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var houseQuoteRequest = conn.Query<HouseQuoteRequest>(select_query, new { quoteRequestId }).FirstOrDefault();
                houseQuoteRequest.HouseLocation = FindHouseLocationById(houseQuoteRequest.HouseLocationId);
                return houseQuoteRequest;
            }

        }

        private HouseLocation FindHouseLocationById(int houseLocationId)
        {
            var select_query = $@"SELECT id AS {nameof(HouseLocation.Id)},
                                    postalcode AS {nameof(HouseLocation.PostalCode)},
                                    city_id AS {nameof(HouseLocation.CityId)},
                                    province_id AS {nameof(HouseLocation.ProvinceId)},
                                    street AS {nameof(HouseLocation.Street)},
                                    appartement_units AS {nameof(HouseLocation.AppartementUnits)}
                                  FROM public.house_location WHERE id=@houseLocationId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var houseLocation = conn.Query<HouseLocation>(select_query, new { houseLocationId }).FirstOrDefault();
                return houseLocation;
            }

        }

    }
}
