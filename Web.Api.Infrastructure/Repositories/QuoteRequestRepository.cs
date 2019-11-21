using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
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
        private readonly string _connectionString;

        public QuoteRequestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;

        }
        public async Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest)
        {

            var addHouseLocationQuery = $@"INSERT INTO public.house_location (postalcode, city, province_id, address, apartment_unit)
                               VALUES (@PostalCode, @City, @ProvinceId, @Address, @ApartmentUnit)
                               RETURNING id;";

            var addHouseQuoteRequest = $@"INSERT INTO public.quote_request_house (user_id, house_type_id, house_location_id, listing, created_date, down_payment, offer, first_house, description, municipal_evaluation)
                               VALUES (@UserId, @HouseType, @HouseLocationId, @ListingPrice, @CreatedDate, @DownPayment, @Offer, @FirstHouse, @Description, @MunicipalEvaluationUrl)
                               RETURNING id;";

            var addDocumentQuoteRequest = $@"INSERT INTO public.quote_request_document (quote_request_id, document_id)
                                           VALUES (@QuoteId, @DocumentId);";




            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
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


                    houseQuoteRequest.DocumentsId.ForEach(x => conn.Execute(addDocumentQuoteRequest, new { QuoteID = quoteRequestId, DocumentId = x }));

                    transaction.Commit();

                    return new HouseQuoteRequestCreateRepoResponse(FindQuoteRequestById(quoteRequestId), true);
                }
                catch (NpgsqlException e)
                {
                    transaction.Rollback();
                    // return the response
                    return new HouseQuoteRequestCreateRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public async Task<HouseQuoteRequestFetchAllRepoResponse> GetAllQuotes()
        {
            var select_query = $@"SELECT id AS {nameof(HouseQuoteRequest.Id)},
                                    user_id AS {nameof(HouseQuoteRequest.UserId)},
                                    house_type_id AS {nameof(HouseQuoteRequest.HouseType)},
                                    house_location_id AS {nameof(HouseQuoteRequest.HouseLocationId)},
                                    created_date AS {nameof(HouseQuoteRequest.CreatedDate)}, 
                                    listing AS {nameof(HouseQuoteRequest.ListingPrice)},
                                    down_payment AS {nameof(HouseQuoteRequest.DownPayment)},
                                    offer AS {nameof(HouseQuoteRequest.Offer)},
                                    first_house AS {nameof(HouseQuoteRequest.FirstHouse)},
                                    description AS {nameof(HouseQuoteRequest.Description)},
                                    municipal_evaluation AS {nameof(HouseQuoteRequest.MunicipalEvaluationUrl)}
                                  FROM public.quote_request_house";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    var response = conn.Query<HouseQuoteRequest>(select_query).ToList();
                    response.ForEach(x => x.HouseLocation = FindHouseLocationById(x.HouseLocationId));

                    return new HouseQuoteRequestFetchAllRepoResponse(response, true);
                }
                catch (Exception e)
                {
                    return new HouseQuoteRequestFetchAllRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<HouseQuoteRequestFetchAllRepoResponse> GetAllQuoteRequestsForUser(string userId)
        {
            var select_query = $@"SELECT id AS {nameof(HouseQuoteRequest.Id)},
                                    user_id AS {nameof(HouseQuoteRequest.UserId)},
                                    house_type_id AS {nameof(HouseQuoteRequest.HouseType)},
                                    house_location_id AS {nameof(HouseQuoteRequest.HouseLocationId)},
                                    created_date AS {nameof(HouseQuoteRequest.CreatedDate)}, 
                                    listing AS {nameof(HouseQuoteRequest.ListingPrice)},
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
                    houseQuoteRequests.ForEach(x => x.Documents = FindDocumentsFor(x.Id).ToList());
                    return new HouseQuoteRequestFetchAllRepoResponse(houseQuoteRequests, true);
                }
                catch (Exception e)
                {
                    return new HouseQuoteRequestFetchAllRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }


        public async Task<HouseQuoteRequestGetDetailResponse> GetDetailFor(int quoteRequestId)
        {
            return new HouseQuoteRequestGetDetailResponse(FindQuoteRequestById(quoteRequestId), true);
        }

        private HouseQuoteRequest FindQuoteRequestById(int quoteRequestId)
        {
            var select_query = $@"SELECT id AS {nameof(HouseQuoteRequest.Id)},
                                    user_id AS {nameof(HouseQuoteRequest.UserId)},
                                    house_type_id AS {nameof(HouseQuoteRequest.HouseType)},
                                    house_location_id AS {nameof(HouseQuoteRequest.HouseLocationId)},
                                    created_date AS {nameof(HouseQuoteRequest.CreatedDate)}, 
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

                var quoteRequestDocuments = FindDocumentsFor(houseQuoteRequest.Id);
                houseQuoteRequest.DocumentsId = quoteRequestDocuments.Select(x => x.Id).ToList();
                houseQuoteRequest.Documents = quoteRequestDocuments.ToList();
                houseQuoteRequest.HouseLocation = FindHouseLocationById(houseQuoteRequest.HouseLocationId);
                return houseQuoteRequest;
            }
        }

        private IEnumerable<File> FindDocumentsFor(int quoteRequestId)
        {
            var select_query = $@"SELECT
                                  id as { nameof(File.Id) },
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE id = @quoteRequestId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var documents = conn.Query<File>(select_query, new { quoteRequestId });
                return documents;
            }
        }

        private HouseLocation FindHouseLocationById(int houseLocationId)
        {
            var select_query = $@"SELECT id AS {nameof(HouseLocation.Id)},
                                    postalcode AS {nameof(HouseLocation.PostalCode)},
                                    city AS {nameof(HouseLocation.City)},
                                    province_id AS {nameof(HouseLocation.ProvinceId)},
                                    address AS {nameof(HouseLocation.Address)},
                                    apartment_unit AS {nameof(HouseLocation.ApartmentUnit)}
                                  FROM public.house_location WHERE id=@houseLocationId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var houseLocation = conn.Query<HouseLocation>(select_query, new { houseLocationId }).FirstOrDefault();
                return houseLocation;
            }

        }

        public async Task<HouseQuoteRequestDeleteRepoResponse> Delete(int quoteRequestId)
        {
            var delete_document_query = $@"DELETE FROM public.quote_request_document WHERE id=@quoteRequestId";
            var delete_request_query = $@"DELETE FROM public.quote_request_house WHERE id=@quoteRequestId";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // fetch quote request
                    var response = FindQuoteRequestById(quoteRequestId);
                    // delete document request
                    conn.Execute(delete_document_query, new { quoteRequestId });
                    // delete quote request
                    var success = Convert.ToBoolean(conn.Execute(delete_request_query, new { quoteRequestId }));
                    // commit
                    transaction.Commit();
                    // return the response
                    return new HouseQuoteRequestDeleteRepoResponse(response, success);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // return the response
                    return new HouseQuoteRequestDeleteRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<HouseQuoteRequestUpdateRepoResponse> Update(int quoteRequestId, HouseQuoteRequest houseQuoteRequest)
        {
            var delete_document_query = $@"DELETE FROM public.quote_request_document WHERE quote_request_id = @id";

            var insert_document_query = $@"INSERT INTO public.quote_request_document (quote_request_id, document_id)
                                           VALUES (@quoteRequestId, @documentId)";

            var update_house_query = $@"UPDATE public.house_location
                                        SET postalcode = @postalCode,
                                            city = @city,
                                            province_id = @provinceId,
                                            address = @address,
                                            apartment_unit = @apartmentUnit
                                        WHERE id = @id";

            var update_request_query = $@"UPDATE public.quote_request_house
                                          SET house_type_id = @houseTypeId,
                                              listing = @listing,
                                              down_payment = @downPayment,
                                              offer = @offer,
                                              first_house = @firstHouse,
                                              description = @description,
                                              municipal_evaluation = @municipalEvaluation
                                          WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();

                try
                {
                    //get house location id
                    var houseLocationID = FindQuoteRequestById(quoteRequestId).HouseLocationId;
                    // delete all documents for request
                    conn.Execute(delete_document_query, new { id = quoteRequestId });
                    // insert all updated documents
                    houseQuoteRequest.DocumentsId.ForEach(x => conn.Execute(insert_document_query, new { quoteRequestId, documentId = x }));
                    // update house location
                    conn.Execute(update_house_query,
                        new
                        {
                            id = houseLocationID,
                            postalCode = houseQuoteRequest.HouseLocation.PostalCode,
                            city = houseQuoteRequest.HouseLocation.City,
                            provinceId = houseQuoteRequest.HouseLocation.ProvinceId,
                            address = houseQuoteRequest.HouseLocation.Address,
                            apartmentUnit = houseQuoteRequest.HouseLocation.ApartmentUnit
                        });
                    // update house request
                    conn.Execute(update_request_query,
                        new
                        {
                            id = quoteRequestId,
                            houseTypeId = houseQuoteRequest.HouseType,
                            listing = houseQuoteRequest.ListingPrice,
                            downPayment = houseQuoteRequest.DownPayment,
                            offer = houseQuoteRequest.Offer,
                            firstHouse = houseQuoteRequest.FirstHouse,
                            description = houseQuoteRequest.Description,
                            municipalEvaluation = houseQuoteRequest.MunicipalEvaluationUrl
                        });

                    // commit
                    transaction.Commit();
                    // return the response
                    return new HouseQuoteRequestUpdateRepoResponse(FindQuoteRequestById(quoteRequestId), true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // return the response
                    return new HouseQuoteRequestUpdateRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }

            }
        }
    }
}

