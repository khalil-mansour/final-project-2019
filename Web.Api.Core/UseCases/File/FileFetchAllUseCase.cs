using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileFetchAllUseCase : IFileFetchAllUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public FileFetchAllUseCase(IConfiguration configuration, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(FileFetchAllRequest message, IOutputPort<FileFetchAllResponse> outputPort)
        {
            var response = await _fileRepository.FetchAll(message.UserId);

            try
            {
                foreach (var res in response.Files)
                    res.Url = SignUrl(res.StorageId);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error signing the URLs.");
                outputPort.Handle(new FileFetchAllResponse(new Error(e.HResult.ToString(), "Error signing the URLs.")));
                return true;
            }

            outputPort.Handle(response.Success ? new FileFetchAllResponse(response.Files, true) : new FileFetchAllResponse(new Error(response.Errors.First()?.Code, "Error attempting to fetch all user files.")));

            if (!response.Success)
                logger.Error(response.Errors.First().Description);

            return response.Success;
        }

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
