using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestCreateUseCase : IHouseQuoteRequestCreateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IFileRepository _fileRepository;
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IConfiguration _configuration;

        public HouseQuoteRequestCreateUseCase(IConfiguration configuration, IQuoteRequestRepository quoteRequestReposiroty, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _configuration = configuration;
            _quoteRequestRepository = quoteRequestReposiroty;
        }

        public async Task<bool> HandleAsync(HouseQuoteRequestCreateRequest message, IOutputPort<HouseQuoteRequestCreateResponse> outputPort)
        {
            var response = await _quoteRequestRepository.Create(
                new HouseQuoteRequest(message.UserId, message.HouseType,
                new HouseLocation(message.HouseLocationRequest.PostalCode,
                message.HouseLocationRequest.City, 
                message.HouseLocationRequest.ProvinceId,
                message.HouseLocationRequest.Address,
                message.HouseLocationRequest.ApartmentUnit),
                message.ListingPrice, 
                DateTime.Now,
                message.DownPayment,
                message.Offer,
                message.FirstHouse, 
                message.Description, 
                message.DocumentsId,
                message.MunicipalEvaluationUrl));

            if (response.Success)
            {
                // instanciate list
                response.HouseQuoteRequest.Documents = new List<Domain.Entities.File>();
                // fetch files
                response.HouseQuoteRequest.DocumentsId.ForEach(x => response.HouseQuoteRequest.Documents.Add(_fileRepository.GetFile(x)));
                // sign url
                response.HouseQuoteRequest.Documents.ForEach(y => y.Url = SignUrl(y.StorageId));
                // return response
                outputPort.Handle(new HouseQuoteRequestCreateResponse(response.HouseQuoteRequest, true));
            }
            else
            {
                logger.Error(response.Errors.First()?.Description);
                outputPort.Handle(new HouseQuoteRequestCreateResponse(new[] { new Error("Action Failed", "Unable to create house quote request") }));
            }

            return response.Success;
        }

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
