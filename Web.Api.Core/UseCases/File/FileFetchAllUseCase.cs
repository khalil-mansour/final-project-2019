using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileFetchAllUseCase : IFileFetchAllUseCase
    {
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
            foreach (var res in response.Files)
                res.Url = SignUrl(res.StorageId);

            outputPort.Handle(response.Success ? new FileFetchAllResponse(response.Files, true) : new FileFetchAllResponse(response.Errors));
            return response.Success;
        }

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
