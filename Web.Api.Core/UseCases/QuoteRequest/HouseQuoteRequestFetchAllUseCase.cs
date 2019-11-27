using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestFetchAllUseCase : IHouseQuoteRequestFetchAllUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public HouseQuoteRequestFetchAllUseCase(IConfiguration configuration, IQuoteRequestRepository quoteRequestReposiroty, IUserRepository userRepository, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
            _quoteRequestRepository = quoteRequestReposiroty;
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(HouseQuoteRequestFetchAllRequest message, IOutputPort<HouseQuoteRequestFetchAllResponse> outputPort)
        {
            // return user to check his type
            var user = await _userRepository.FindById(message.UserId);

            // response object
            HouseQuoteRequestFetchAllRepoResponse response = new HouseQuoteRequestFetchAllRepoResponse(null, false);

            if (user != null)
            {
                // if client, fetch his quote requests
                if (user.User.UserType == 1)
                    response = await _quoteRequestRepository.GetAllQuoteRequestsForUser(message.UserId);
                // if broker, fetch all _available_ quote requests
                else
                    response = await _quoteRequestRepository.GetAllQuotes();
            }
            else
            {
                outputPort.Handle(new HouseQuoteRequestFetchAllResponse(new[] { new Error("Action Failed", "No corresponding user with matching ID.") }));
                return true;
            }

            if (response.Success)
            {
                // instanciate list
                response.HouseQuoteRequests.ForEach(y => y.Documents = new List<Domain.Entities.File>());
                // fetch files
                response.HouseQuoteRequests.ForEach(x => x.DocumentsId.ForEach(y => x.Documents.Add(_fileRepository.GetFile(y))));
                // sign urls
                response.HouseQuoteRequests.ForEach(x => x.Documents.ForEach(y => y.Url = SignUrl(y.StorageId)));
                // return response
                outputPort.Handle(new HouseQuoteRequestFetchAllResponse(response.HouseQuoteRequests, true));
            }
            else
            {
                logger.Error(response.Errors.First().Description);
                outputPort.Handle(new HouseQuoteRequestFetchAllResponse(new[] { new Error("Action Failed", "Unable to fetch house quote requests.") }));
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
