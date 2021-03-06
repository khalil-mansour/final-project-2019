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
    public sealed class FileFetchUseCase : IFileFetchUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public FileFetchUseCase(IConfiguration configuration, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public async Task<bool> HandleAsync(FileFetchRequest message, IOutputPort<FileFetchResponse> outputPort)
        {
            var response = await _fileRepository.Fetch(message.FileID);

            if (response.Success)
            {
                try
                {
                    response.File.Url = SignUrl(response.File.StorageId);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error signing the URL.");
                    outputPort.Handle(new FileFetchResponse(new[] { new Error("Ation Failed", "Failed to acquire signature for file.") }));
                    return true;
                }
            }

            outputPort.Handle(response.Success ? new FileFetchResponse(response.File, true) : new FileFetchResponse(new[] { new Error("Ation Failed", "Error attempting to fetch a user.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
