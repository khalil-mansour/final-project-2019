using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileFetchUseCase : IFileFetchUseCase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public FileFetchUseCase(IConfiguration configuration, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(FileFetchRequest message, IOutputPort<FileFetchResponse> outputPort)
        {
            var response = await _fileRepository.Fetch(message.StorageId);
            
            // set the signed url to file
            response.File.Url = SignUrl(message.StorageId);

            outputPort.Handle(response.Success ? new FileFetchResponse(response.File, true) : new FileFetchResponse(response.Errors));
            return response.Success;
        }

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
