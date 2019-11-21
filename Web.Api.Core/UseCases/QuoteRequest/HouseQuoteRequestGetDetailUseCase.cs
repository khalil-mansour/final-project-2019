using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestGetDetailUseCase : IHouseQuoteRequestGetDetailRequestUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public HouseQuoteRequestGetDetailUseCase(IConfiguration configuration, IQuoteRequestRepository quoteRequestReposiroty, IFileRepository fileRepository) {
            _configuration = configuration;
            _quoteRequestRepository = quoteRequestReposiroty;
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestGetDetailRequest message, IOutputPort<HouseQuoteRequestGetDetailResponse> outputPort)
        {            
            var response = await _quoteRequestRepository.GetDetailFor(message.QuoteRequestId);

            if (response.Success)
            {
                // instanciate list
                response.HouseQuoteRequest.Documents = new List<Domain.Entities.File>();
                // fetch files
                response.HouseQuoteRequest.DocumentsId.ForEach(x => response.HouseQuoteRequest.Documents.Add(_fileRepository.GetFile(x)));
                // sign url
                response.HouseQuoteRequest.Documents.ForEach(x => x.Url = SignUrl(x.StorageId));
                // return response
                outputPort.Handle(new HouseQuoteRequestGetDetailResponse(response.HouseQuoteRequest, true));
            }
            else
            {
                logger.Error(response.Errors.First().Description);
                outputPort.Handle(new HouseQuoteRequestGetDetailResponse(new[] { new Error("Action Failed", "Unable to get detail for house quote request.") }));
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
